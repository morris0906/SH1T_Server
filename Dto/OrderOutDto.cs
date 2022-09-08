using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A2.Dto
{
    public class OrderOutDto
    {
        public int Id { get; set; }
        public string userName { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
