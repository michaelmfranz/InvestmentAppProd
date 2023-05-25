using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentApp.Persistence
{

    public interface IInvestmentRespository : IGenericRepository<Investment>
    {
        IEnumerable<Investment> Find(string name);
    }

}
