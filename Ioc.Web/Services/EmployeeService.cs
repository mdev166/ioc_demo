﻿using IoC.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace IoC.Web.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly List<Employee> _employees = new List<Employee>();
        private readonly ILoggerService _logger;

        public EmployeeService(ILoggerService logger)
        {
            // additionally, could extend this by injecting a data respository
            //  and and unit test this service

            // log- Info, Warning or Error
            _logger = logger;

            InitDemoData();
        }

        /// <summary>
        /// Get sales employees
        /// </summary>
        /// <returns></returns>
        public IList<Employee> GetEmployees()
        {
            _logger.Log("GetEmployees");

            return _employees;
        }

        /// <summary>
        /// Get sales emplee by id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public Employee GetEmployee(int employeeId)
        {
            _logger.Log("GetEmployee");

            return _employees.FirstOrDefault(e => e.Id == employeeId);
        }

        /// <summary>
        /// Set up demo data
        /// </summary>
        private void InitDemoData()
        {
            _employees.Add(new Employee { Id = 1002, FirstName = "Maria", LastName = "Alvarez" });
            _employees.Add(new Employee { Id = 1008, FirstName = "Mitch", LastName = "Benson" });
            _employees.Add(new Employee { Id = 1010, FirstName = "Jerry", LastName = "Johnson" });
            _employees.Add(new Employee { Id = 1012, FirstName = "Kate", LastName = "Thompson" });
            _employees.Add(new Employee { Id = 1013, FirstName = "Margaret", LastName = "Sorenstein" });
            _employees.Add(new Employee { Id = 1024, FirstName = "Carlos", LastName = "Vega" });
            _employees.Add(new Employee { Id = 1029, FirstName = "Mordecai", LastName = "Rigby" });
            _employees.Add(new Employee { Id = 1031, FirstName = "Steve", LastName = "Williams" });
        }
    }
}