using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ProcedureResults
{
    public class RemittanceCurrencyLimitResult
    {
        public int Id { get; set; }
        public decimal DD_Rate { get; set; }
        public Guid UID { get; set; }
        public decimal Amount { get; set; }
        public string ComparisonName { get; set; }
    }
}
