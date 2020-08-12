using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class EmployeeDatasController : ApiController
    {
        public IEnumerable<EmployeeData> Get()
        {
            using(EmployeeDBEntities entities =new EmployeeDBEntities())
            {
                return entities.EmployeeDatas.ToList();
            }
        }


        public EmployeeData Get(int id)
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                return entities.EmployeeDatas.FirstOrDefault(e => e.emp_id == id);
            }
        }

       /* public void Post([FromBody] EmployeeData employeeData)
        {
            using(EmployeeDBEntities entities=new EmployeeDBEntities())
            {
                entities.EmployeeDatas.Add(employeeData);
                entities.SaveChanges();
            }
        }*/

        public HttpResponseMessage Post(EmployeeData employeeData)
        {
            HttpResponseMessage result;
            if (employeeData!=null)
            {
                using(EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    entities.EmployeeDatas.Add(employeeData);
                    entities.SaveChanges();
                }
                result = Request.CreateResponse(HttpStatusCode.Created, employeeData);

            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest, "Server Failed");
            }
            return result;
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {

                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var entity = entities.EmployeeDatas.FirstOrDefault(e => e.emp_id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with ID =" + id.ToString() + "Not Found to Delete");
                    }
                    else
                    {
                        entities.EmployeeDatas.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateErrorResponse(HttpStatusCode.OK,"Well");
                    }

                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id,[FromBody] EmployeeData employeeData)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var entity = entities.EmployeeDatas.FirstOrDefault(e => e.emp_id == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with ID=" + id.ToString() + "not found to update");
                    }
                    else
                    {
                        entity.emp_id = employeeData.emp_id;
                        entity.emp_name = employeeData.emp_name;
                        entity.emp_email = employeeData.emp_email;
                        entity.emp_role = employeeData.emp_role;
                        entity.emp_joining_date = employeeData.emp_joining_date;
                        entity.emp_salary = employeeData.emp_salary;


                        entities.SaveChanges();
                        return Request.CreateErrorResponse(HttpStatusCode.OK,"Welldone");
                    }
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
