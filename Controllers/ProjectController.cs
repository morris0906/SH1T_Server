using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A2.Data;
using A2.Dto;
using A2.Models;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace A2.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly IProjectRepo _repository;
        public ProjectController(IProjectRepo repository)
        {
            _repository = repository;
        }

        //POST/api/Register
        [HttpPost("Register")]
        public ContentResult Register(UserInDto user)
        {
            Users u = new()
            {
                UserName = user.UserName,
                Password = user.Password,
                Address = user.Address
            };
            string result;
            if (_repository.AddUser(u))
            {
                string success = "User successfully registered.";
                result = success;
            }
            else
            {
                string fail = "Username not available.";
                result = fail;
            }
            ContentResult contentResult = new()
            {
                Content = result,
                ContentType = "text/plain;charset=utf-8",
                StatusCode = (int)HttpStatusCode.OK,
            };
            return contentResult;
        }

        //GET/api/GetVersionA
        [Authorize(AuthenticationSchemes = "UserAuthentication")]
        [Authorize(Policy = "UserOnly")]
        [HttpGet("GetVersionA")]
        public ContentResult ValidLogin()
        {
            ContentResult contentResult = new()
            {
                Content = "V1",
                ContentType = "text/plain;charset=utf-8",
                StatusCode = (int)HttpStatusCode.OK,
            };
            return contentResult;
        }

        //POST/api/PurchaseItem
        [Authorize(AuthenticationSchemes = "UserAuthentication")]
        [Authorize(Policy = "UserOnly")]
        [HttpPost("PurchaseItem")]
        public ActionResult<OrderOutDto> Purchase(OrderInDto order)
        {
            ClaimsIdentity ci = HttpContext.User.Identities.FirstOrDefault();
            Claim c = ci.FindFirst("UserName");
            string UserName = c.Value;
            UserOrders userOrders = new()
            {
                UserName = UserName,
                ProductId = order.ProductId,
                Quantity = order.Quantity,
            };
            UserOrders added = _repository.AddOrder(userOrders);
            OrderOutDto oo = new()
            {
                Id = added.Id,
                userName = added.UserName,
                ProductId = added.ProductId,
                Quantity = added.Quantity
            };
            return CreatedAtAction(nameof(GetUser), new { id = oo.Id }, oo);
        }

        //GET/api/PurchaseSingleItem/{productid}
        [Authorize(AuthenticationSchemes = "UserAuthentication")]
        [Authorize(Policy = "UserOnly")]
        [HttpGet("PurchaseSingleItem/{ProductId}")]
        public ActionResult<OrderOutDto> PurchaseSingleItem(int ProductId)
        {
            ClaimsIdentity ci = HttpContext.User.Identities.FirstOrDefault();
            Claim c = ci.FindFirst("UserName");
            string UserName = c.Value;
            UserOrders userOrders = new()
            {
                UserName = UserName,
                ProductId = ProductId,
                Quantity = 1,
            };
            UserOrders added = _repository.AddOrder(userOrders);
            OrderOutDto oo = new()
            {
                Id = added.Id,
                userName = added.UserName,
                ProductId = added.ProductId,
                Quantity = added.Quantity
            };
            return CreatedAtAction(nameof(GetUser), new { id = oo.Id }, oo);
        }

        //GET/api/GetUser
        [Authorize(AuthenticationSchemes = "UserAuthentication")]
        [Authorize(Policy = "UserOnly")]
        [HttpGet("GetUser")]
        public ActionResult<UserOutDto> GetUser()
        {
            ClaimsIdentity ci = HttpContext.User.Identities.FirstOrDefault();
            Claim c = ci.FindFirst("UserName");
            Users user = _repository.GetUser(c.Value);
            UserOutDto u = new()
            {
                UserName = user.UserName,
                Password = user.Password,
                Address = user.Address
            };
            return Ok(u);
        }

        //TEST
        [Authorize(AuthenticationSchemes = "UserAuthentication")]
        [Authorize(Policy = "UserOnly")]
        [HttpGet("Test")]
        public ActionResult Test()
        {
            ClaimsIdentity ci = HttpContext.User.Identities.FirstOrDefault();
            Claim c = ci.FindFirst("userName");
            string userName = c.Value;
            Users user = _repository.GetUser(userName);
            UserOutDto userOut = new()
            {
                UserName = user.UserName,
                Password = user.Password,
                Address = user.Address
            };
            return Ok(userOut);
        }
    }
}
