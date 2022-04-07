using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Application.Contracts.CustomerDiscount
{
    public class DefineCostumerDiscount
    {
        public long ProductId { get; set; }
        public int DiscountRate { get;set; }
        public string StartDate { get;set; }
        public string EndDate { get; set; }
        public string Reason { get; set; }
    }
}
