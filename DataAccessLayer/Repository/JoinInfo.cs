using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class JoinInfo
    {
        public string JoinType { get; set; } // INNER, LEFT, RIGHT, etc.
        public string TargetTable { get; set; }
        public string JoinCondition { get; set; }
    }
}
