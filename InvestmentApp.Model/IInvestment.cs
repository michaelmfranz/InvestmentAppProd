using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentApp.Persistence
{
    public interface IInvestment
    {
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public string InterestType { get; set; }

        public double InterestRate { get; set; }

        public double PrincipalAmount { get; set; }

        public double CurrentValue { get; set; }

        void CalculateValue();
    }
}
