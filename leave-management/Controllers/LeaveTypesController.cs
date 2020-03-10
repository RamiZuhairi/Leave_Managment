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
using Microsoft.AspNetCore.Mvc;
/// <summary>
/// please check the the explain in Google drive 
/// this is our first controller ,Add- controller - read and Write controller 
/// 1- First we will add 
/// 2- Controllers has acctions and those actions are retuening content based on name 
/// 3- By adding the ILeaveTypeRepostitory,IMapper this is what dependncy injection is whitch is mean when we refrence interface but that interface know the functions and call them to repository class 
/// 
/// </summary>
namespace leave_management.Controllers
{
    [Authorize(Roles = "Administrator")] //this will be more security ,they need to login before we get down ,they cant just add the code(L23,M11:09) 
    public class LeaveTypesController : Controller
    {
        private readonly ILeaveTypeRepostitory _repo;//just dont forget when you use private we can use underscore_ to lead us to know (must be readOnly)
        private readonly IMapper _mapper;//just dont forget when you use private we can use underscore_ to lead us to know 
       
        
        // now e need to do constructor method , because we have private vars ,JUST REMMBER WHENEVER WE HAVE PRIVATE WE HAVE CONSTRUCTOR 
        public LeaveTypesController(ILeaveTypeRepostitory repo, IMapper mapper)//we pass 2 parmetors 
        {
            _repo = repo;
            _mapper = mapper;
        }
        
        // GET: LeaveTypes
        public ActionResult Index() // when we request action for index , we will get index fun called return View(); / then it will go to Views foloder,check the same name if the Controller (Home)HomeController then  then home then index.cshtml and retuen the content of that file 
        {// here when we work with index 
            var leavetypes = _repo.FindAll().ToList();
            var model = _mapper.Map<List<LeaveType>, List<LeaveTypeVM>>(leavetypes);
            return View(model);
        }

        // GET: LeaveTypes/Details/5
        public ActionResult Details(int id)//we need to check if the request exist , then we processed the data 
        {
          if(!_repo.IsExists(id))
            {
                return NotFound();
            }
            var leavetype = _repo.FindById(id);
            var model = _mapper.Map<LeaveTypeVM>(leavetype);
            return View(model);
        }

        // GET: LeaveTypes/Create
        public ActionResult Create()// here we will generate view based on CreateViewModel of LeaveTypeVM
        {
            return View();
        }

        // POST: LeaveTypes/Create
        [HttpPost]// this will our post 
        [ValidateAntiForgeryToken]
        public ActionResult Create(LeaveTypeVM model)
        {
            try
            {
                // TODO: Add insert logic here
                if(!ModelState.IsValid)
                {
                    return View(model);
                }
              
                var leaveType = _mapper.Map<LeaveType>(model);
                leaveType.DateCreated = DateTime.Now;
                var isSuccess = _repo.Create(leaveType);
                if(!isSuccess)
                {
                    ModelState.AddModelError("","Something went Wrong...");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something went Wrong...");
                return View(model);
            }
        }

        // GET: LeaveTypes/Edit/5
        public ActionResult Edit(int id)
        {
            // first Edit need to know if there is any records first
            if(!_repo.IsExists(id))
            {
                return NotFound();// this is nice 404 error to tell user 
            }
            var leavetype = _repo.FindById(id);
            var model = _mapper.Map<LeaveTypeVM>(leavetype);// always must be mapper 

            return View(model);
        }

        // POST: LeaveTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LeaveTypeVM model)
        {// what we doing in Edit same what we done in Create very easy only change update fun
            try
            {
                // TODO: Add update logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var leaveType = _mapper.Map<LeaveType>(model);
                leaveType.DateCreated = DateTime.Now;
                var isSuccess = _repo.Update(leaveType);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something went Wrong...");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));//we can redirect to another page if we want after pdate 
            }
            catch
            {
                ModelState.AddModelError("", "Something went Wrong...");
                return View(model);
            }
        }

        // GET: LeaveTypes/Delete/5
        public ActionResult Delete(int id)
        {
            var leavetype = _repo.FindById(id);// find eleement by id 
            if (leavetype == null)
            {
                return NotFound();
            }
            var isSuccess = _repo.Delete(leavetype);// delete i t
            if (!isSuccess)
            {
                ModelState.AddModelError("", "Something went Wrong...");
                return BadRequest();// this is another kind of error 
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: LeaveTypes/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, LeaveTypeVM model)
        //{
        //    try
        //    {

            

        //       //we can redirect to another page if we want after pdate 
        //    }
        //    catch
        //    {
        //        ModelState.AddModelError("", "Something went Wrong...");
        //        return View(model);
        //    }
        //}
    }
}