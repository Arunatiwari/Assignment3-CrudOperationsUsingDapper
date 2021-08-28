using Assignment3_CrudOperationsUsingDapper.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DalUsingDapper;
using Bol;

namespace Assignment3_CrudOperationsUsingDapper.Controllers
{
    public class PersonController : Controller
    {
        public DatabaseHealper _dbH;
        public PersonController(DatabaseHealper _dbH)
        {
            this._dbH = _dbH;
        }

        public IActionResult Index()
        {
            List<Person> Persons = new List<Person>();
            using (IDbConnection db = new SqlConnection("Server = LAPTOP-M03HOU1E\\SQLEXPRESS; Database = EFCore_Dapper; Trusted_Connection = True; MultipleActiveResultSets = true"))
            {
                Persons = db.Query<Person>("Select * From Person").ToList();
            }

            return View(Persons);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Person model)
        {
            try
            {
                var inseted = _dbH.Insert(model);
                if (inseted)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            catch (Exception)
            {
            }

            return View();
        }

        public IActionResult Edit(int Id)
        {
            var persons = _dbH.Get_Data(Id);
            return View("Create", persons);
        }

        [HttpPost]
        public IActionResult Edit(Person model)
        {
            try
            {
                var updated = _dbH.Update(model);
                if (updated)
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Error", "Home");
            }
            catch (Exception)
            {

            }

            return View("Create", model);
        }

        public IActionResult Delete(int Id)
        {
            var deleted = _dbH.Delete(Id);
            return RedirectToAction("Index");
        }
    }
}
