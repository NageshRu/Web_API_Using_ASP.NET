using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class ViewDatasController : ApiController
    {
        public IEnumerable<ViewData> Get()
        {
            using(EmployeeDBEntities entities=new EmployeeDBEntities())
            {
                return entities.ViewDatas.ToList();
            }
        }

        public ViewData Get(int id)
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                return entities.ViewDatas.FirstOrDefault(e => e.emp_id == id);
            }
        }
    }
}
