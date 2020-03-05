using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data
{
    public class LeaveAllocation
    {
        [Key]
        public int Id { get; set; }
        public int NumberOfDays { get; set; }
        public DateTime DateCreated { get; set; }
        [ForeignKey("EmployeeId")] // will be the forent key of leaveAlocation and its the primary key of employee to connect  
        public Employee Employee { get; set; }//we put Employee class because we want to know the employee detials  
        public string EmployeeId { get; set; }// to know the employeeID string or int we go back to SQL explorer and check the colomn
        
        
        [ForeignKey("LeaveTypeId")]
        public LeaveType LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
    }
}
