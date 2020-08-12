using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class EmployeeSecurity
    {
        public static bool Login(string username,string pass)
        {
            using(EmployeeDBEntities entities=new EmployeeDBEntities())
            {
                return entities.LoginUsers.Any(user => user.email.Equals(username, StringComparison.OrdinalIgnoreCase) && user.password == pass);
            }
        }
    }
}