using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnluCo.Hafta1.Odev.Entities;

namespace UnluCo.Hafta1.Odev.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private static List<Student> StudentList = new List<Student>()
        {
            new Student {Id = 1,Name ="Fatih",Surname="Topcu",Email="fatih@mail.com",SchoolId=1},
            new Student {Id = 2,Name ="Arif",Surname="Paşa",Email="arif@mail.com",SchoolId=2},
            new Student {Id = 3,Name ="Celal",Surname="Bayar",Email="Celal@mail.com",SchoolId=3}
           
        };
        [HttpGet]
        public IActionResult GetSchools()
        {
            var studentList = StudentList.OrderBy(x => x.Id);

            return Ok(studentList);

        }
        [HttpGet("{colon}/{orderby}")]
        public IActionResult GetSchools(string colon = "id", string orderby = "asc")
        {
            var studentList = StudentList;
            if (colon.ToLower() == "id" && orderby.ToLower() == "asc")
            {
                studentList = StudentList.OrderBy(x => x.Id).ToList();
            }
            else if (colon.ToLower() == "id" && orderby.ToLower() == "desc")
            {
                studentList = StudentList.OrderByDescending(x => x.Id).ToList();
            }
            else if (colon.ToLower() == "name" && orderby.ToLower() == "asc")
            {
                studentList = StudentList.OrderBy(x => x.Name).ToList();
            }
            else if (colon.ToLower() == "name" && orderby.ToLower() == "desc")
            {
                studentList = StudentList.OrderByDescending(x => x.Name).ToList();
            }
            else
            {
                BadRequest("Yanlış sıralama tercihleri yapılıdı.");
            }


            return Ok(studentList);

        }

        [HttpGet("id")]
        public IActionResult GetSchoolById(int id)
        {
            var student = StudentList.SingleOrDefault(x => x.Id == id);
            if (student == null)
                return NotFound("Öğrenci Bulunamadı.");
            return Ok(student);
        }
        [HttpPost]
        public IActionResult AddSchool([FromBody] Student newStudent)
        {
            var student = StudentList.SingleOrDefault(x => x.Id == newStudent.Id);
            if (student != null)
                return NotFound("Eklenmek istenen öğrenci bulunuyor");
            StudentList.Add(newStudent);
            return Created("Öğrenci Eklendi", newStudent);

        }
        [HttpPut("id")]
        public IActionResult UpdateSchool(int id, [FromBody] Student newStudent)
        {
            var student = StudentList.SingleOrDefault(x => x.Id == id);
            if (student == null)
                return NotFound("Güncellenmek İstenen Öğrenci Bulunamadı.");

            student.Name = newStudent.Name != default ? newStudent.Name : student.Name;
            student.Surname = newStudent.Surname != default ? newStudent.Surname : student.Surname;
            student.Email = newStudent.Email != default ? newStudent.Email : student.Email;
            return Ok(student);
        }
        [HttpDelete("id")]
        public IActionResult DeleteSchool(int id)
        {
            var student = StudentList.SingleOrDefault(x => x.Id == id);
            if (student == null)
                return NotFound("Öğrenci Bulunamadı.");
            StudentList.Remove(student);
            return Ok(student);
        }


    }
}
