using leave_management.Contracts;
using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/// <summary>
/// this we will create class inside the Repository and 
/// 
/// // reason why we created this class is to define the 4  oprations (CRUD) that created in IRepostitoryBase interface so to do that we 
/// 1-  LeaveTypeRepostitory inhert from  ILeaveTypeRepostitory , press F12 to see from where it coming
/// reason why :
/// 2- when we inhert we need to do 2 things 1- is the get name space 2- we need to implement all functions, so we do Implemt interface to get the rest of the functons as you see blow in the code  
/// 3- so now if we need to test that we can add another fun to base class will give us error in LeaveTypeRepostitory class unitl we implemnt the new fun we addded 
/// Basicly Base class will hold all functions then we need to implement it 
/// 4- if we add any funtion to ILeaveTypeRepostitory it wil allso cmllain that we need to define that fun in LeaveTypeRepostitory
/// 5- after we connec the classes all , we need to let the Databe know , so what we do is we will create object refrese to ApplicationDbContext
/// 6- we need conctroctor to out class so we press on ctor then Tap Tap ,, so what we need it to do is oop is to take the value of _db because its private 
/// so the steps to create dependncey injections is  :
/// 
/// first step
/// 1- we will create objecty type ApplicationDbContext _db
/// 2- we will create consctrotor method and assighn the private var to the conctorcor method var 
/// second Step
/// 1- you must represent what we done above in every single class we have , so we copy it there 
/// </summary>
namespace leave_management.Repostitory
{
    
    public class LeaveTypeRepostitory : ILeaveTypeRepostitory
    {
        private readonly ApplicationDbContext _db;
        //conctroctor
        public LeaveTypeRepostitory(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool Create(LeaveType entity)// now this is more fun, when we create leave type , need to accept the data and put it in db 
        {
            //throw new NotImplementedException(); delete 
            _db.LeaveTypes.Add(entity);//means we bring leaveTypes then Add new entiry to it , we called it entity in base class  
         //Save-- we need to save later - the fun return bool
            return Save();
        }

        public bool Delete(LeaveType entity)
        {
            // throw new NotImplementedException();
            _db.LeaveTypes.Remove(entity);
            // we need to save 
            return Save();

        }

        public ICollection<LeaveType> FindAll()// first fun in the base class /// hold on ctrl click it will take us to defnition of that class 
        {
            //throw new NotImplementedException(); we will delete it 
            //then we have to say from db select all leave types , because Icollection return all filed , and we have to return value of Leavetpyes then put them in list then in var so we can return them 
            // just quick note .. we used _db that has access to all ApplicationDbContext Daaset we have added there 
            var LeaveTypes = _db.LeaveTypes.ToList();// we chose list because its very flixeble collection 
            return LeaveTypes;
            //as we see ICollection is list so we need to return list 
        }

        public LeaveType FindById(int id)// in this one we need to see the type of leave and who aplyed for it 
        {
            // as we see this is funtion reaturn only one object type LeaveType , we find that object by uniqe id from DB
            //throw new NotImplementedException();
            var LeaveType = _db.LeaveTypes.Find(id);//fun Find very nice to return what we looking for by id
            return LeaveType;

        }

        public ICollection<LeaveType> GetEmployeesByLeaveType(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsExists(int id)
        {
            // throw new NotImplementedException();
            //var exists = _db.LeaveTypes.Any();//this is nice way to check if the table is empty 
            var exists = _db.LeaveTypes.Any(q => q.Id == id);// means i there is any Id of lemada is equl to id parmeter 
            return exists;
        }

        public bool Save()
        {
            //throw new NotImplementedException();
            
            //now we need to return bool > 0 thats mean in every opration CRUD we do one opration at the time, so we compare if its grater than 0 
            var changes = _db.SaveChanges();
            return changes > 0;// return changes if its grater than 0
            //now we can use this fun in all others create update delete 
        }

        public bool Update(LeaveType entity)// to do update we will follow the same pattrn 
        {
            //  throw new NotImplementedException(); delete this one 
            _db.LeaveTypes.Update(entity);
            //we need to save.
            return Save();

        }
    }
}
