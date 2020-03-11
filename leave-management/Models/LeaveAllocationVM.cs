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
      
        public int NumberOfDays { get; set; }
        public DateTime DateCreated { get; set; }
        public int Period { get; set; }
        public EmployeeVM Employee { get; set; }//this is return one entity called Employee,we get them from View model we have created  EmployeeVM, and we will create IEnumerable for it
        public string EmployeeId { get; set; }
        public LeaveTypeVM LeaveType { get; set; }//this is return  one entiry called LeaveType ,we get them from View model we have created  , we wil create IEnumerable for it 
        public int LeaveTypeId { get; set; }

      
        
      
    }
    public class CreateLeaveAllocationVM
    {

   
           public int NumberUpdated { get; set; }
           public List< LeaveTypeVM> LeaveTypes { get; set; }
     }
    public class EditLeaveAllocationVM
    {
        public int Id { get; set; }
       
        public EmployeeVM Employee { get; set; }
        public string EmployeeId { get; set; }
        [Display( Name="Number Of Days")]
        public int NumberOfDays { get; set; }
        public LeaveTypeVM LeaveType { get; set; }
       
    }
    public class ViewAllocationVM
    {


        public EmployeeVM Employee { get; set; }
        public string EmployeeId { get; set; }
        public List<LeaveAllocationVM> LeaveAllocations { get; set; }
    }
}
