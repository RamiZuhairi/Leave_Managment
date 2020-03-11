using AutoMapper;
using leave_management.Data;
using leave_management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/// <summary>
/// As we said before , Each Data clas of DB have veiew model VM so we can do validation and other staff , but we have to do mapping , and its very easy look down below 
/// 1- we will inhert from Profile and add using Automapping
/// 2- we will add cunsttrctor, inside the cuntroctor we will create the maps 
/// we have to create the map between source and destanation.
/// after we finish we must let the core initialize what we have done , when the application is running, so we do that in startUp.sc
/// </summary>
namespace leave_management.Mappings
{
    public class Maps:Profile
    {
        public Maps()
        {
            
            CreateMap<LeaveType, LeaveTypeVM>().ReverseMap(); // source is LeaveType, destnation is DetailsLeaveTypeVM, ReverseMap()will reverse the opration
            CreateMap<LeaveAllocation, LeaveAllocationVM>().ReverseMap();
            CreateMap<LeaveAllocation, EditLeaveAllocationVM>().ReverseMap();
            CreateMap<LeaveHistory, LeaveHistoryVM>().ReverseMap();
            CreateMap<Employee, EmployeeVM>().ReverseMap();
            


        }
    }
}
