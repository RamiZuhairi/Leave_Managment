using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class LeaveRequestVM
    {
        public int Id { get; set; }
        [Display(Name = "Reference Number")]
        public EmployeeVM RequestingEmployee { get; set; }
        [Display(Name ="Employee Name")]
        public string RequestingEmployeeId { get; set; }
        [Display(Name = "Start Date")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public LeaveTypeVM LeaveType { get; set; }
        public int LeaveTypeId { get; set; }

        [Display(Name = "Date Requested")]
        public DateTime DateRequested { get; set; }
        [Display(Name = "Date Actioned")]
        public DateTime DateActioned { get; set; }
        public bool? Approved { get; set; }
        public EmployeeVM ApprovedBy { get; set; }
        [Display(Name = "Approver Name")]
        public string ApprovedById { get; set; }
        [Display(Name = "Employee Comments")]
        [MaxLength(300)]
        public string RequestComments { get; set; }
        public bool Cancelled { get; set; }


    }
    public class AdminLeaveRequestViewVM
    {
        [Display(Name = "Total Requests")]
        public int TotalRequests { get; set; }
        [Display(Name = "Approved Requests")]
        public int ApprovedRequests { get; set; }
        [Display(Name = "Pending Requests")]
        public int PendingRequests { get; set; }
        [Display(Name = "Rejected Requests")]
        public int RejectedRequests { get; set; }

        public List<LeaveRequestVM> LeaveRequests { get; set; }// L35,M10:00 also very nice to represent the list above by putting it in public list 
    }
    public class CreateLeaveRequestVM
    {
        [Display(Name = "Start Date")]
        [Required]
        public DateTime StartDate { get; set; }
       
        [Display(Name = "End Date")]
        [Required]
        public DateTime EndDate { get; set; }

        public IEnumerable<SelectListItem> LeaveTypes { get; set; }
        
        [Display(Name = " Leave Type")]
        public int LeaveTypeId { get; set; }
        public string RequestComments { get; set; }
        //public string Name { get; set; }
    }
    public class EmployeeLeaveResquestViewVM
    {
        public List<LeaveAllocationVM> LeaveAllocations { get; set; }
        public List<LeaveRequestVM> LeaveRequests { get; set; }
        //public string RequestComments { get; set; }
        //public bool Cancelled { get; set; }
    }

}
