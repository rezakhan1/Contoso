using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Contoso.Data;
using Contoso.Domain;

namespace Contoso.API.Controllers
{
    public class StudentController : ApiController
    {
        private ContosoContext _context;
        private ContosoContext1 _context1;

        public List<Student> GetStudents()
        {
            _context = new ContosoContext();
            return _context.Students.ToList();
        }

        public Student GetStudentById(int id)
        {
            _context = new ContosoContext();
            return _context.Students.Find(id);
        }

        public IHttpActionResult PostStudent(Student student)
        {
            _context = new ContosoContext();
            _context.Students.Add(student);
            _context.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = student.Id }, student);
        }
        //public IHttpActionResult PostStudent(Student student) {

        //    _context1 = new ContosoContext1();
        //    _context1.Students.Add(student);
        //    _context1.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new  { id = student.Id }, student);
        //}

        public IHttpActionResult PutStudent(Student student)
        {
            _context = new ContosoContext();
            _context.Entry(student).State = EntityState.Modified;
            _context.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult DeleteStudent(int id)
        {
            _context = new ContosoContext();
            var student = _context.Students.Find(id);
            _context.Students.Remove(student);
            _context.SaveChanges();
            return Ok(student);
        }

        [HttpPost]
        [Route("api/upload-student-photo/{studentId}")]
        public IHttpActionResult UploadStudentPhoto(int studentId)
        {
            _context = new ContosoContext();
            var student = _context.Students.Find(studentId);

            string destinationPath = "Contos.Web/Documents/";
           
            HttpFileCollection files = HttpContext.Current.Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFile file = files[i];
                if (file.ContentLength > 0)
                {
                    string fileName = new FileInfo(file.FileName).Name;
                    string imagePath = @"./" + destinationPath + "/" + fileName;
                    string fileExtension = new FileInfo(file.FileName).Extension;

                    if (fileExtension == ".jpg" || fileExtension == ".png")
                    {
                        Guid id = Guid.NewGuid();
                        fileName = id.ToString() + "_" + fileName;
                        try
                        {
                            file.SaveAs(destinationPath + Path.GetFileName(fileName));
                        }
                        catch (Exception exception)
                        {
                            var message = exception.Message;
                            return Ok(message);
                        }
                        student.Photo = "Documents/" + fileName;
                        _context.Entry(student).State = EntityState.Modified;
                        _context.SaveChanges();
                        return Ok("Ok");
                    }

                }
            }
            return Ok("Photo could not be uploaded!");
        }
    }
}
