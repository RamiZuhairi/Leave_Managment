using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Data
{
    public class Employee : IdentityUser// the empoyee is the same user so we can use inhert to take the data from user table 
        // so if we want the props below to join the user table we will inhert from IdentityUser
    {
        // first way to create this is to write prop then press TaB x2
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string TaxId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateJoined { get; set; }

    }
}
