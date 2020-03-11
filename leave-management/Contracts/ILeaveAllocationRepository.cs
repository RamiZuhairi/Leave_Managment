using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Contracts
{
   public interface ILeaveAllocationRepository:IRepostitoryBase<LeaveAllocation>// must be public otherwise it will not show 
    {
        bool CheckAllocation(int leavetypeid, string employeeid);// we have to implement it in
        ICollection<LeaveAllocation> GetLeaveAllocationsByEmployee(string id);

    }
}
