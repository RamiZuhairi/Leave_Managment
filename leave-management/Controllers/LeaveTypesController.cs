using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
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
        public ActionResult Details(int id)
        {
          
            return View();
        }

        // GET: LeaveTypes/Create
        public ActionResult Create()// here we will generate view based on CreateViewModel of LeaveTypeVM
        {
            return View();
        }

        // POST: LeaveTypes/Create
        [HttpPost]// this will our post 
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                if(!ModelState.IsValid)
                {
                    return View(collection);
                }
                // we need to map it as well to the data type we want
                var leaveType = _mapper.Map<LeaveType>(collection);
                leaveType.DateCreated = DateTime.Now;//we dont requir the user to do that we , we will set the data of now 
                var isSuccess =  _repo.Create(leaveType);// this will return bool if its not success the form will not be submited 
                if(!isSuccess)
                {
                    ModelState.AddModelError("","Something went Wrong...");// ModelState has all type of errores we want to display to user 
                    return View(collection);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveTypes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LeaveTypes/Edit/5
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

        // GET: LeaveTypes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveTypes/Delete/5
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