using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace A2.Models
{
    public class UserOrders
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
