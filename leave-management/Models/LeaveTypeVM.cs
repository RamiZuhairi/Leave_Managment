using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    /// <summary>
    /// so we 1- copies the properties from Data file class and add it here 
    /// 2- so why do we we nedd View model , it will give us flixiblity when we cn chaew not all the ptoperties that we have like id ,name etc , to customize what user have  
    /// 3- to inforce certin rechecks , for validations and requird proerty    [Required]
    /// 4- you can have diffrent view models for diffrent perpouses 
    /// 5- reason why we doing the hard work because that is the write way to do it to get letss headache in the future 
    /// </summary>
    public class LeaveTypeVM
    {
        public int Id { get; set; } 
        [Required]
        public string Name { get; set; }
        
        [Required]
        [Display(Name ="Default Number Of Days")]
        [Range(1,25,ErrorMessage ="Please enter valid number between(1-25)!")]
        public int DefaultDays { get; set; }
        [Display(Name ="Date Created")]
        public DateTime? DateCreated { get; set; }// ? mean is nullable so it it will not presure me to provde value for it 
    }



}
