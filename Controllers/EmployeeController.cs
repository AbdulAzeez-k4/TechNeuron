using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using CRUD.Models;
using CRUD.DB;

namespace MVCCRUD.Controllers
{
	public class EmployeeController : Controller
	{
		string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\BISMILLA\source\repos\CRUD\CRUD\DB\Database.mdf;Integrated Security=True";
		// GET: Employee
		public ActionResult Index()
		{
			return View();
		}

		#region CRUD
		[HttpGet]
		public ActionResult Create()
		{
			return View();
		}

		#region CRUD
		/// <summary>
		/// Creates a new record for Employee
		/// </summary>
		/// <param name="empModel">EmpModel object as params</param>
		/// <returns>Returns id of the employee</returns>
		public int Create(EmployeeModel empModel)
		{
            // Assuming YourDbContext is your Entity Framework DbContext class
            using (CRUDDBContext context = new CRUDDBContext(ConnectionString))
            {
                // Create a new Employee entity
                EmployeeDomain newEmployee = new EmployeeDomain
                {
                    Name = empModel.Name,
                    Age = empModel.Age,
					DateOfJoining = empModel.DateOfJoin

                };

                // Add the new employee to the context
                context.Employee.Add(newEmployee);

                // Save changes to the database
                context.SaveChanges();

                // Access the ID of the newly inserted record
                int newEmployeeId = newEmployee.Id;
                return newEmployeeId;
                // Assuming your Employee entity has an 'Id' property
            }

            
		}

		/// <summary>
		/// Gets all employee details as list of employees
		/// </summary>
		/// <returns>Returns List of EmpModel</returns>
		public IActionResult Read()
		{
            List<EmployeeModel> empList;

            using (var context = new CRUDDBContext(ConnectionString)) // Assuming CRUDDBContext is your DbContext
            {
                empList = context.Employee
                    .Select(e => new EmployeeModel
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Age = e.Age
                    })
                    .ToList();
            }

			ViewData["Employees"] = empList;

            return View();
		}

		/// <summary>
		/// Updates the Employee for the given id
		/// </summary>
		/// <param name="empModel">Employee model object</param>
		/// <returns>Returns int if successfully updated</returns>
		public IActionResult Update(EmployeeModel empModel)
		{

			using (CRUDDBContext dbContext = new CRUDDBContext(ConnectionString)) // Replace YourDbContext with your actual DbContext class
			{
				// Retrieve the employee from the database
				EmployeeDomain employeeToUpdate = dbContext.Employee.Find(empModel.Id);

				if (employeeToUpdate != null)
				{
					// Update the employee properties
					employeeToUpdate.Name = empModel.Name;
					employeeToUpdate.Age = empModel.Age;

					// Save changes to the database
					dbContext.SaveChanges();
				}
				List<EmployeeModel> empList;


				using (var context = new CRUDDBContext(ConnectionString)) // Assuming CRUDDBContext is your DbContext
				{
					empList = context.Employee
						.Select(e => new EmployeeModel
						{
							Id = e.Id,
							Name = e.Name,
							Age = e.Age
						})
						.ToList();
				}

				ViewData["Employees"] = empList;

				return View("Read");
			}
		}

		/// <summary>
		/// Deletes the employee for the given id
		/// </summary>
		/// <param name="empModel">EmpModel object</param>
		/// <returns></returns>
		public IActionResult Delete(EmployeeModel empModel)
		{
            using (CRUDDBContext dbContext = new CRUDDBContext(ConnectionString)) // Replace YourDbContext with your actual DbContext class
            {
                // Retrieve the employee from the database
                EmployeeDomain employeeToDelete = dbContext.Employee.Find(empModel.Id);

                if (employeeToDelete != null)
                {
                    // Remove the employee from the DbSet
                    dbContext.Employee.Remove(employeeToDelete);

                    // Save changes to the database
                    dbContext.SaveChanges();

					
                    //return Json(new { success = true, message = "Employee deleted successfully" });
                }
                List<EmployeeModel> empList;


                using (var context = new CRUDDBContext(ConnectionString)) // Assuming CRUDDBContext is your DbContext
                {
                    empList = context.Employee
                        .Select(e => new EmployeeModel
                        {
                            Id = e.Id,
                            Name = e.Name,
                            Age = e.Age
                        })
                        .ToList();
                }

                ViewData["Employees"] = empList;

                return View("Read");
            }
        }
		#endregion
		#endregion
	}
}