using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace InvestmentApp.Persistence
{
    //The following GenericRepository class Implement the IGenericRepository Interface
    //And Here T is going to be a class
    //While Creating an Instance of the GenericRepository type, we need to specify the Class Name
    //That is we need to specify the actual class name of the type T
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public OurDBContext context = null;
 
        public DbSet<T> table = null;


        public GenericRepository()
        {
            DbContextOptions<OurDBContext> dbContextOptions = new DbContextOptionsBuilder<OurDBContext>().UseInMemoryDatabase(databaseName: "InvestmentsDbTest").Options;
            this.context = new OurDBContext(dbContextOptions);
            table = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }


        public void Insert(T obj)
        {
            context.ChangeTracker.Clear();
            table.Add(obj);
            context.SaveChanges();
        }

        public void Update(T obj)
        {
            context.ChangeTracker.Clear();
            context.Entry(obj).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}

