using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using HW26__23_05_2020.Models;

namespace HW26__23_05_2020.Controllers
{
    public class PersonController : Controller
    {
        private string conString = "Data Source=MACHINE-PGO7H84;Initial Catalog=MobileStoreDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(conString))
                {
                    var personList = db.Query<Person>("SELECT * FROM PERSON").ToList();
                    return View(personList);
                }
            }
            catch
            {

            }
            return View(null);

        }

        //Select Person id
        public IActionResult SelectId()
        {

            return View();
        }

        [HttpPost]
        public IActionResult SelectById(int? Id)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(conString))
                {
                    var personListbyId = db.Query<Person>($"SELECT * FROM PERSON WHERE Id = '{Id}' ").ToList();
                    return View(personListbyId);
                }
            }
            catch
            {

            }
            return View(null);

        }

        //Add Person
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(string LastName, string FirstName, string MiddleName)
        {
            var model = new Person()
            {

                LastName = LastName,
                FirstName = FirstName,
                MiddleName = MiddleName
            };
            using (IDbConnection db = new SqlConnection(conString))
            {
                db.Execute("INSERT INTO PERSON( [LastName], [FirstName], [MiddleName]) " +
                    $"VALUES('{model.LastName}', '{model.FirstName}', '{model.MiddleName}')");
            }
            return RedirectToAction("Index");
        }

        //Select FullName 
        public IActionResult SelectFullName()
        {

            return View();
        }

        [HttpPost]
        public IActionResult SearchFullName(string LastName, string FirstName, string MiddleName)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(conString))
                {
                    var personListbyFullName = db.Query<Person>($"SELECT * FROM PERSON  WHERE LastName like '%{LastName}%' AND FirstName like '%{FirstName}%' AND MiddleName like '%{MiddleName}%' ").ToList();
                    return View(personListbyFullName);
                }
            }
            catch
            {

            }
            return View(null);
        }

    }
}