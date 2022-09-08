using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A2.Models;

namespace A2.Data
{
    public interface IProjectRepo
    {
        public IEnumerable<Users> GetAllUsers();
        public bool ValidLogin(string userName, string password);
        public Users GetUser(string user);
        public bool AddUser(Users user);
        public void SaveChanges();
        public IEnumerable<Products> GetProducts();
        public UserOrders AddOrder(UserOrders order);
    }
}
