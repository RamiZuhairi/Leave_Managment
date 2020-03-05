using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{/// <summary>

 ///       please check the explaination in LeaveType 
 ///  public Employee Employee { get; set; } //here the compiler gave us red line because he want us to refance it from datamodel(we dont wamt to refrence data class to view model ) but we have to refrence it from data model so what we do ? we have to create them  
 /// public LeaveType LeaveType { get; set; }
 /// GOLDEN RULES :data class should talk to data classes and Viw model should talk to view model , thats why we need to create view model to employee
 /// so power view model : this is all what user need to see only 
 /// </summary>
    public class LeaveAllocationVM
    {
        public int Id { get; set; }
        [Required]
        public int NumberOfDays { get; set; }
        public DateTime DateCreated { get; set; }
       
        public EmployeeVM Employee { get; set; }//this is return one entity called Employee,we get them from View model we have created  EmployeeVM, and we will create IEnumerable for it
        public string EmployeeId { get; set; }
        public DetailsLeaveTypeVM LeaveType { get; set; }//this is return  one entiry called LeaveType ,we get them from View model we have created  , we wil create IEnumerable for it 
        public int LeaveTypeId { get; set; }

        //while the  Employee,EmployeeId get collected we must have kind of datacollection like array to gather that data and add it to IEnumerable ()data collection , same to LeaveTypes
        public IEnumerable<SelectListItem> Employees { get; set; }// SelectListItem is the Data type and the Emplyees is the name, thats represent the drop down list 
        public IEnumerable<SelectListItem> LeaveTypes { get; set; }//drop down list where the user need to select   
    }
}
