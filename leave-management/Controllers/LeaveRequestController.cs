using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace leave_management.Controllers
{
    [Authorize]// means no one can access unless they registered in system 
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveRequestRepository _leaveRequestRepo;
        private readonly ILeaveTypeRepostitory _leaveTypeRepo;
        private readonly ILeaveAllocationRepository _leaveAllocationReop;
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;


        public LeaveRequestController(ILeaveRequestRepository leaveRequestRepo
            ,ILeaveTypeRepostitory leaveTypeRepo, IMapper mapper, ILeaveAllocationRepository leaveAllocationReop
            , UserManager<Employee> userManager)
        {
            _leaveRequestRepo = leaveRequestRepo;
            _leaveTypeRepo = leaveTypeRepo;
            _mapper = mapper;
            _leaveAllocationReop = leaveAllocationReop;
            _userManager = userManager;
        }


        [Authorize(Roles = "Administrator")]// we can set authorization to admin view only
        // GET: LeaveRequest
       
        public ActionResult Index()//where the Employee can see the request , and for the admin when see all leave requests
        {
            var leaveRequests = _leaveRequestRepo.FindAll();
            var leaveRequestModels = _mapper.Map < List<LeaveRequestVM>>(leaveRequests);
            var model = new AdminLeaveRequestViewVM
            {
                TotalRequests = leaveRequestModels.Count(),
                ApprovedRequests = leaveRequestModels.Count(q => q.Approved == true), //L35,M16:22 we can see many ways to represent count 
                PendingRequests = leaveRequestModels.Count(q => q.Approved == null),
                RejectedRequests = leaveRequestModels.Count(q => q.Approved == false),
                LeaveRequests = leaveRequestModels //L35:M19 for leave rquest we already got it from above
            };
            return View(model);
        }

        public ActionResult MyLeave()
        {
            var employee = _userManager.GetUserAsync(User).Result;
            var employeeid = employee.Id;
            var employeeAllocations = _leaveAllocationReop.GetLeaveAllocationsByEmployee(employeeid);
            var empoyeeRequests = _leaveRequestRepo.GetLeaveRequestByEmployee(employeeid);

            var employeeAllocationModel = _mapper.Map<List<LeaveAllocationVM>>(employeeAllocations);
            var employeeRequestModel = _mapper.Map<List<LeaveRequestVM>>(empoyeeRequests);

            var model = new EmployeeLeaveResquestViewVM
            {
                LeaveAllocations = employeeAllocationModel,
                LeaveRequests = employeeRequestModel
            };

            return View(model);
        }

        // GET: LeaveRequest/Details/5
        public ActionResult Details(int id)
        {
            var leaveRequest = _leaveRequestRepo.FindById(id);
            var model = _mapper.Map<LeaveRequestVM>(leaveRequest);

            return View(model);
        }
        public ActionResult ApproveRequest(int id)
        {
            try
            {
            var user = _userManager.GetUserAsync(User).Result;
            var leaveRequest = _leaveRequestRepo.FindById(id);
            var employeeid = leaveRequest.RequestingEmployeeId;
            var leaveTypeId = leaveRequest.LeaveTypeId;
            var allocation = _leaveAllocationReop.GetLeaveAllocationsByEmployeeAndType(employeeid, leaveTypeId);
            int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;//L39,M29
              //  allocation.NumberOfDays -=  daysRequested;// the same down one
                allocation.NumberOfDays = allocation.NumberOfDays - daysRequested;

                leaveRequest.Approved = true;
            leaveRequest.ApprovedById = user.Id;
            leaveRequest.DateActioned = DateTime.Now;

            _leaveRequestRepo.Update(leaveRequest);
                _leaveAllocationReop.Update(allocation);
                return RedirectToAction(nameof(Index));
          
            }
            catch (Exception)
            {

                return RedirectToAction(nameof(Index));
            }
            

        }
        public ActionResult RejectRequest(int id)
        {
            try
            {
                var user = _userManager.GetUserAsync(User).Result;
                var leaveRequest = _leaveRequestRepo.FindById(id);
                leaveRequest.Approved = false;
                leaveRequest.ApprovedById = user.Id;
                leaveRequest.DateActioned = DateTime.Now;

                _leaveRequestRepo.Update(leaveRequest);
              
                    return RedirectToAction(nameof(Index));
               
            }
            catch (Exception)
            {

                return RedirectToAction(nameof(Index));
            }
        }


        // GET: LeaveRequest/Create
        public ActionResult Create()
        {
            var leaveTypes = _leaveTypeRepo.FindAll();
            var leaveTypeItems = leaveTypes.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });
            var model = new CreateLeaveRequestVM
            {
                LeaveTypes = leaveTypeItems
            };
            return View(model);
        }

        // POST: LeaveRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateLeaveRequestVM model)
        {
            
            try
            {
            var startDate = Convert.ToDateTime(model.StartDate);
            var endDate = Convert.ToDateTime(model.EndDate);
            var leaveTypes = _leaveTypeRepo.FindAll();
            var leaveTypeItems = leaveTypes.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });
            model.LeaveTypes = leaveTypeItems;
                if(!ModelState.IsValid)
                {
                    return View(model);
                }
                if (DateTime.Compare(startDate, endDate) > 1)//L38,M18
                {
                    ModelState.AddModelError("", "Start date cannot be further to the future than the end date!");
                    return View(model);
                }

                var employee = _userManager.GetUserAsync(User).Result;//L38,M4.40 very nice way to get Eployee who login
                var allocation = _leaveAllocationReop.GetLeaveAllocationsByEmployeeAndType(employee.Id, model.LeaveTypeId);
                int daysRequested = (int)(endDate - startDate).TotalDays;//L38,M19
                if(daysRequested > allocation.NumberOfDays)
                {
                    ModelState.AddModelError("", "You Do Not Have Suffiicient Days For This Request");
                    return View(model);
                }

                var leaveRequestModel = new LeaveRequestVM
                {
                    RequestingEmployeeId= employee.Id,
                    StartDate= startDate,
                    EndDate= endDate,
                    Approved=null,
                    DateRequested= DateTime.Now,
                    DateActioned=DateTime.Now, 
                    LeaveTypeId= model.LeaveTypeId,
                    RequestComments= model.RequestComments 

    };
                var leaveRequest = _mapper.Map<LeaveRequest>(leaveRequestModel);//L38,M25:20
                var isSuccess = _leaveRequestRepo.Create(leaveRequest);
                
                if(!isSuccess)
                {
                    ModelState.AddModelError("", "Somethng Went Wrong With Submitting Your Record!");
                    return View(model);
                }
                return RedirectToAction("MyLeave");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "Somethng Went Wrong With Submitting Your Record!");
                return View(model);
            }
        }
        public ActionResult CancelRequest(int id)
        {
            var user = _userManager.GetUserAsync(User).Result;
            var leaveRequestid = _leaveRequestRepo.FindById(id);
            var employeeid = leaveRequestid.RequestingEmployeeId;
            var leaveTypeId = leaveRequestid.LeaveTypeId;
            var allocation = _leaveAllocationReop.GetLeaveAllocationsByEmployeeAndType(employeeid, leaveTypeId);
            int daysRequested = (int)(leaveRequestid.EndDate - leaveRequestid.StartDate).TotalDays;

            allocation.NumberOfDays = allocation.NumberOfDays + daysRequested;
            var leaveRequest = _leaveRequestRepo.FindById(id);
            leaveRequest.Cancelled = true;
            _leaveRequestRepo.Update(leaveRequest);//run the update query
            return RedirectToAction("MyLeave");
        }

        // GET: LeaveRequest/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LeaveRequest/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveRequest/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveRequest/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}