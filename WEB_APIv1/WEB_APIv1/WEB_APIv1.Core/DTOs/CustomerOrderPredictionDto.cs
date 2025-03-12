using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB_APIv1.Core.DTOs
{
    public class CustomerOrderPredictionDto
    {
        public string CustomerName { get; set; }
        public DateTime? LastOrderDate { get; set; }
        public DateTime? NextPredictedOrder { get; set; }
    }
}
