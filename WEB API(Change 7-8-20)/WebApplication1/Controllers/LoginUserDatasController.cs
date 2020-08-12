using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class LoginUserDatasController : ApiController
    {
        public IEnumerable<LoginUser> Get()
        {
            using(EmployeeDBEntities entities =new EmployeeDBEntities())
            {
                return entities.LoginUsers.ToList();
            }
        }
    }
}
