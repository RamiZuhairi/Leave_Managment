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

namespace leave_management.Controllers
{  
    [Authorize(Roles = "Administrator")] //this will be more security ,they need to login before we get down ,they cant just add the code(L23,M11:09) 
    public class LeaveAllocationController : Controller
    {
        private readonly ILeaveTypeRepostitory _leaverepo;
        private readonly ILeaveAllocationRepository _leaveallocationrepo; 
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;



        public LeaveAllocationController(ILeaveTypeRepostitory leaverepo, ILeaveAllocationRepository leaveallocationrepo, IMapper mapper, UserManager<Employee> userManager)
        {
            _leaverepo = leaverepo;
            _leaveallocationrepo = leaveallocationrepo;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: LeaveAllocation
        public ActionResult Index()
        {
            var leavetypes = _leaverepo.FindAll().ToList();
            var mappedLeaveTypes = _mapper.Map<List<LeaveType>, List<LeaveTypeVM>>(leavetypes);
            var model = new CreateLeaveAllocationVM
            {
                LeaveTypes = mappedLeaveTypes ,
                NumberUpdated = 0
            };
           return View(model);
        }
        //SeatLeave
        public ActionResult SetLeave(int id)
        {
            var leavetype = _leaverepo.FindById(id);
            var employees = _userManager.GetUsersInRoleAsync("Employee").Result;
            foreach(var emp in employees)
            {
                if (_leaveallocationrepo.CheckAllocation(id, emp.Id))//L28,M35, nice way to check if its already exist then stop, 
                    continue;
                var allocation = new LeaveAllocation
                {
                    DateCreated = DateTime.Now,
                    EmployeeId = emp.Id,
                    LeaveTypeId = id,
                    //DateUpdated = leavetype.DateCreated,
                    NumberOfDays = leavetype.DefaultDays,
                    Period = DateTime.Now.Year
                };
                var leavallocation = _mapper.Map<LeaveAllocation>(allocation);
                _leaveallocationrepo.Create(leavallocation);

            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult ListEmployees()
        {
            var employees = _userManager.GetUsersInRoleAsync("Employee").Result;
            var model = _mapper.Map<List<EmployeeVM>>(employees);

            return View(model);
        }


        // GET: LeaveAllocation/Details/5
        public ActionResult Details(string id)
        {
            var employee = _mapper.Map<EmployeeVM>(_userManager.FindByIdAsync(id).Result);//always async fun requred result fun L30,M4:00//we can do all of that in one line intead of make var to mapper 

            var allocations = _mapper.Map<List<LeaveAllocationVM>>(_leaveallocationrepo.GetLeaveAllocationsByEmployee(id));//return list of allocations
            var model = new ViewAllocationVM
            {
                Employee = employee,
                LeaveAllocations = allocations
        };
            return View(model);
        }

        // GET: LeaveAllocation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveAllocation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveAllocation/Edit/5
        public ActionResult Edit(int id)
        {
            var leaveallocation = _leaveallocationrepo.FindById(id);
            var model = _mapper.Map<EditLeaveAllocationVM>(leaveallocation);//L31,M7:00 why we refer to EditLeaveAllocationVM, not in to LeaveAllocationVM, because we ceatered fun up there make the code more orginze
            return View(model);
        }

        // POST: LeaveAllocation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditLeaveAllocationVM model)
        {
            try
            {
                // TODO: Add update logic here
                if(!ModelState.IsValid)
                {
                    return View(model);
                }
                var record = _leaveallocationrepo.FindById(model.Id);
                record.NumberOfDays = model.NumberOfDays;
               var  isSuccess =  _leaveallocationrepo.Update(record);
                if(!isSuccess)
                {
                    ModelState.AddModelError("","Error while saveing");
                    return View(model);
                }

                return RedirectToAction(nameof(Details),new {id=model.EmployeeId });
            }
            catch
            {
                return View(model);
            }
        }

        // GET: LeaveAllocation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveAllocation/Delete/5
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