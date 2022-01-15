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
    public class SchoolController : ControllerBase
    {
        private static List<School> SchoolList = new List<School>()
        {
            new School {Id = 1,Name ="Ataşehir Anadolu"},
            new School {Id = 2,Name ="Kayışdağı ArifPaşa"},
            new School {Id = 3,Name ="Celal Bayar"},
        };
        
       
        [HttpGet]
        public IActionResult GetSchools() 
        {
            var schoolList = SchoolList.OrderBy(x => x.Id);

            return Ok(schoolList);

        }
        [HttpGet("{colon}/{orderby}")]
        public IActionResult GetSchools(string colon = "id", string orderby = "asc")
        {
            var schoolList = SchoolList;
            if (colon.ToLower() == "id" && orderby.ToLower() == "asc")
            {
                schoolList = SchoolList.OrderBy(x => x.Id).ToList();
            }
            else if (colon.ToLower() == "id" && orderby.ToLower() == "desc")
            {
                schoolList = SchoolList.OrderByDescending(x => x.Id).ToList();
            }
            else if (colon.ToLower() == "name" && orderby.ToLower() == "asc")
            {
                schoolList = SchoolList.OrderBy(x => x.Name).ToList();
            }
            else if (colon.ToLower() == "name" && orderby.ToLower() == "desc")
            {
                schoolList = SchoolList.OrderByDescending(x => x.Name).ToList();
            }
            else
            {
                BadRequest("Yanlış sıralama tercihleri yapılıdı.");
            }
           

            return Ok(schoolList);

        }

        [HttpGet("id")]
        public IActionResult GetSchoolById(int id)
        {
            var school = SchoolList.SingleOrDefault(x => x.Id == id);
            if (school == null)
                return NotFound("Okul Bulunamadı.");
            return Ok(school);
        }
        [HttpPost]
        public IActionResult AddSchool([FromBody]School newSchool)
        {
            var school = SchoolList.SingleOrDefault(x => x.Id == newSchool.Id);
            if (school != null)
                return NotFound("Eklenmek istenen okul bulunuyor");
            SchoolList.Add(newSchool);
            return Created("Okul Eklendi",newSchool);

        }
        [HttpPatch("id")]
        public IActionResult UpdateSchool(int id,[FromBody] School newSchool)
        {
            var school = SchoolList.SingleOrDefault(x => x.Id == id);
            if (school == null)
                return NotFound("Güncellenmek İstenen Okul Bulunamadı.");

            school.Name = newSchool.Name != default ? newSchool.Name : school.Name; 
            return Ok(school);
        }
        [HttpDelete("id")]
        public IActionResult DeleteSchool(int id)
        {
            var school = SchoolList.SingleOrDefault(x => x.Id == id);
            if (school == null)
                return NotFound("Okul Bulunamadı.");
            SchoolList.Remove(school);
            return Ok(school);
        }


    }
}
