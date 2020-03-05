using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using leave_management.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
/// <summary>
/// this is our first controller ,Add- controller - read and Write controller 
/// 1- first we will add 
/// </summary>
namespace leave_management.Controllers
{
    
    public class LeaveTypesController : Controller
    {
        private readonly ILeaveTypeRepostitory _repo;//just dont forget when you use private we can use underscore_ to lead us to know 
        private readonly IMapper _mapper;//just dont forget when you use private we can use underscore_ to lead us to know 
       
        
        // now e need to do constructor method , because we have private vars ,JUST REMMBER WHENEVER WE HAVE PRIVATE WE HAVE CONSTRUCTOR 
        public LeaveTypesController(ILeaveTypeRepostitory repo, IMapper mapper)//we pass 2 parmetors 
        {
            _repo = repo;
            _mapper = mapper;
        }
        // GET: LeaveTypes
        public ActionResult Index()
        {
            return View();
        }

        // GET: LeaveTypes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LeaveTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypes/Create
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