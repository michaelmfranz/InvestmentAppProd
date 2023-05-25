using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentApp.Persistence
{
    public class InvestmentRespository : GenericRepository<Investment>, IInvestmentRespository
    {

        public InvestmentRespository(InvestmentApp.Persistence.OurDBContext _context)
        {
            this.context = _context;
            table = context.Set<Investment>();
        }


        public IEnumerable<Investment> Find(string name)
        {
            return context.Investments.Where(x => x.Name == name).ToList();
        }

    }
}
