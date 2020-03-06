using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data
{
    public class LeaveType
    {
        //(Ctrl+ .) it will show us suggestyions same whne we press on the yello light for Qucik Action 
        [Key] // this is requirend in DB to set the primary  key for this file or table  
        public int Id { get; set; } // primary key
        [Required] // means we not allow Nulls in the colmn name 
        public string Name { get; set; }
        [Display(Name = "Date Created")]// just to view in way we want it , thats how to control our view of the colums
        public DateTime DateCreated { get; set; }
    }
}
