using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    /// <summary>
    /// for mapping we will copy the employee proberies and then we add the rest from the SQL Server Object Exporal DB
    /// to succes the mapping we have 2 thing very important , the name must be the same ane data type must be the same 
    ///remmber everything must much , we will ge [Id][UserName][Email][PhoneNumber]
    /// </summary>
    public class EmployeeVM
    {
        public string Id { get; set; }

        public string UserName { get; set; }
         public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
       
        public string TaxId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateJoined { get; set; }
    }
}
