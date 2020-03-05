using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Contracts
{

    /// <summary>
    /// //we have to in hert from it so we can get the 4 oprations in base class 
    /// <<LeaveType>> as you see is class theat we need to intercet with 
    /// using System.Collections.Generic; we need to add this one to the foloder to use LEaveType
    /// ILeaveTypeRepostitory inhert from  IRepostitoryBase that takes <LeaveType> as class to work on data and using the 4 oprations CRUD
    /// 
    /// </summary>
    public interface ILeaveTypeRepostitory : IRepostitoryBase<LeaveType>// what we did here we let the ILeaveTypeRepostitory inhret from IRepostitoryBase because it has all oprations create delete update and save and we put the class name that we wanted to be specific for that class only 
    {
        ICollection<LeaveType> GetEmployeesByLeaveType(int id);// lets say we want to get all employees by type , dont forget to implent it in LeaveTypeRepository

    }
}
