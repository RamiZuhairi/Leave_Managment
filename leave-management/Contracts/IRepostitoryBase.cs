//so now we created interface called IRepostitoryBase, from Add - class- chose interface insead of class  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Contracts
{
    // first we will do this class generic 
   public  interface IRepostitoryBase <T> where T : class // we made it public then we sid where T : class thats mean and class we going to put here it will proforme the oprations 
    {
        ICollection<T> FindAll();// Icollection is array it will take T is your class as generic identifer and find All from DB, return all data in that T table 
        T FindById(int id); // then find what we loocking for by ID (function to find parmater by id )
        bool Create(T entity);// now this is more fun, when we create leave type , need to accept the data and put it in db 
        bool Update(T entity);// to do update we will follow the same pattrn 
        bool Delete(T entity);//now we delete 
        bool Save();
    }
}
