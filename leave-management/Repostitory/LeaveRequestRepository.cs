using leave_management.Contracts;
using leave_management.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repostitory
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly ApplicationDbContext _db;
        //conctroctor
        public LeaveRequestRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool Create(LeaveRequest entity)
        {
            _db.LeaveRequests.Add(entity);
            return Save();
        }

        public bool Delete(LeaveRequest entity)
        {
            _db.LeaveRequests.Remove(entity);
            return Save();

        }

        public ICollection<LeaveRequest> FindAll()
        {
            var leaveRequests = _db.LeaveRequests
                .Include(q => q.RequestingEmployee)
                .Include(q => q.ApprovedBy)
                .Include(q => q.LeaveType)

                .ToList();
            return leaveRequests;
        }


        public LeaveRequest FindById(int id)
        {
           var leaveHistory= _db.LeaveRequests
                .Include(q => q.RequestingEmployee)
                .Include(q => q.ApprovedBy)
                .Include(q => q.LeaveType)
                .FirstOrDefault(q=>q.Id==id);
            return leaveHistory;
        }

        public ICollection<LeaveRequest> GetLeaveRequestByEmployee(string employeeid)
        {
           var leaveRequests=FindAll()
                .Where(q => q.RequestingEmployeeId==employeeid)//L40,M4:20
                .ToList();
            return leaveRequests;
        }

        public bool IsExists(int id)
        {// you find the explaination for this in LeaveTypeRepository.cs
            var exists = _db.LeaveRequests.Any(q => q.Id == id);
            return exists;
        }

        public bool Save()
        {
            var changes=_db.SaveChanges();
            return changes > 0;
        }

        public bool Update(LeaveRequest entity)
        {
            _db.LeaveRequests.Update(entity);
            return Save();
        }
    }
}
