using Contoso.Data;
using Contoso.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Contoso.API.Controllers
{
    public class Student1Controller : ApiController
    {
        // GET: Student1
        public Student  PostStudent (Student students)
        {
            ContosoContext1 context = new ContosoContext1();
            context.Students.Add(students);
            context.SaveChanges();
            return students;
        }
    }
}