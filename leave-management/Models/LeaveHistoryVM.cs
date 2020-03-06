using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
/// <summary>
/// this table accully will restore the leave refrences and deltials , and wether they proved or not 
/// </summary>
namespace leave_management.Models
{// please find the explaination of this class in the LeaveTypeVM we just did the same here 
    public class LeaveHistoryVM
    {
       
        public int Id { get; set; }

       
        public EmployeeVM RequestingEmployee { get; set; }
        public string RequestingEmployeeId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
       
        public LeaveTypeVM LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public IEnumerable<SelectListItem> LeaveTypes { get; set; }// also he datacollection to store LeaveTypeId,LeaveType
        public DateTime DateRequested { get; set; }
        public DateTime DateActioned { get; set; }
        public bool? Approved { get; set; }
        
        public EmployeeVM ApprovedBy { get; set; }
        public string ApprovedById { get; set; }
    }
}
