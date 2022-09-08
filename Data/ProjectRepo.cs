using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A2.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace A2.Data
{
    public class ProjectRepo:IProjectRepo
    {
        private readonly ProjectDbContext _dbContext;

        public ProjectRepo(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Users GetUser(string user)
        {
            Users User = _dbContext.Users.FirstOrDefault(e => e.UserName == user);
            return User;
        }
        public IEnumerable<Users> GetAllUsers()
        {
            IEnumerable<Users> users = _dbContext.Users.ToList();
            return users;
        }

        public bool ValidLogin(string username, string password)
        {
            Users user = _dbContext.Users.FirstOrDefault(e => e.UserName == username && e.Password == password);
            if (user == null)
                return false;
            else
                return true;
        }
        public bool AddUser(Users user)
        {
            Users u = user;
            Users x = _dbContext.Users.FirstOrDefault(e => e.UserName == u.UserName);
            if (x == null)
            {
                EntityEntry<Users> e = _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                return true;
            }
            
            else
                return false;
        }
        public IEnumerable<Products> GetProducts()
        {
            IEnumerable<Products> products = _dbContext.Products.ToList();
            return products;
        }
        public UserOrders AddOrder(UserOrders orders)
        {
            EntityEntry<UserOrders> uo = _dbContext.UserOrders.Add(orders);
            UserOrders order = uo.Entity;
            _dbContext.SaveChanges();
            return order;
        }
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
