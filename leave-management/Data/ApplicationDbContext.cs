using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using leave_management.Models;

namespace leave_management.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        // =================

        /// <summary>
        /// so to make it clear when we add the proberty what we do 
        /// 1- cleate the class and add props , thats will be like coloms in the data base we have to set the keys as well 
        /// 2- we add that class and table name using DbSet to the DataApplicationDbContext in data file 
        /// 3- we will will add migration using PMC, packageMangerConcole add-migration AddLeaveDetailsTables, that very handy when we give another devoer to work for us so he knows where did we add ed the databese process 
        /// 4- we update the dabase to see the new tables 
        /// </summary>
        // we need  there is class name Employees must be part of DbSet 
        // just make sure any class we going to create we must rub it here in db context dont forget
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; } // the blue LeaveType is the name of the class , the black leaveTypes is name od the table 

        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
        public DbSet<LeaveHistory> LeaveHistories { get; set; }
        public DbSet<leave_management.Models.LeaveTypeVM> LeaveTypeVM { get; set; }
        //public DbSet<LeaveTypeVM> LeaveTypeVM { get; set; }
        //public DbSet<LMS.Models.LeaveTypeVM> LeaveTypeM { get; set; }


    }
}
