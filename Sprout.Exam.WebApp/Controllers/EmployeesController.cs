using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Business.BusinessLogic;
using Microsoft.Extensions.Configuration;
using System;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        public EmployeesController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var result = await Task.FromResult(StaticEmployees.ResultList);
            var employee = await Task.FromResult(Employee.GetEmployee(null, false));
            return Ok(employee);
        }

        /// <summary>
        /// Gets employee types  based on the given parameters
        /// </summary>
        /// <returns></returns>
        [HttpPost("{employeetype}")]
        public async Task<IActionResult> GetAllEmployeeType()
        {
            var employeeType = await Task.FromResult(Employee.GetEmployeeType(null));
            return Ok(employeeType);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            //var result = await Task.FromResult(StaticEmployees.ResultList.FirstOrDefault(m => m.Id == id));
            var employee = await Task.FromResult(Employee.GetEmployee(id, false).FirstOrDefault(m => m.Id == id));
            return Ok(employee);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(EditEmployeeDto input)
        {
            //var item = await Task.FromResult(StaticEmployees.ResultList.FirstOrDefault(m => m.Id == input.Id));
            input.IsDeleted = 0;
            var employee = await Task.FromResult(Employee.AddUpdateDeleteEmployee(input).FirstOrDefault());
            if (employee == null) return NotFound();
            employee.FullName = input.FullName;
            employee.Tin = input.Tin;
            employee.Birthdate = input.Birthdate.ToString("yyyy-MM-dd");
            employee.TypeId = input.TypeId;
            return Ok(employee);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeDto input)
        {
            //var id = await Task.FromResult(StaticEmployees.ResultList.Max(m => m.Id) + 1);
            var employee = await Task.FromResult(Employee.AddUpdateDeleteEmployee(input).FirstOrDefault());
            return Ok(employee);
        }


        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //var result = await Task.FromResult(StaticEmployees.ResultList.FirstOrDefault(m => m.Id == id));
            CreateEmployeeDto employeeDto = new CreateEmployeeDto();
            employeeDto.Id = id;
            employeeDto.IsDeleted = 1;
            var employee = await Task.FromResult(Employee.AddUpdateDeleteEmployee(employeeDto).FirstOrDefault());
            if (employee == null) return NotFound();
            return Ok(id);
        }



        /// <summary>
        /// Refactor this method to go through proper layers and use Factory pattern
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rateMultiply"></param>
        /// <returns></returns>
        [HttpPost("{id}/calculate")]
        public async Task<IActionResult> Calculate(CalculateDto input)
        {
            var employee = await Task.FromResult(Employee.GetEmployee(input.Id, false).FirstOrDefault(m => m.Id == input.Id));
            if (employee == null) return NotFound();
            var employeeType = string.Empty;
            string netIncome = "";

            if (employee.IsMonthly == true)
            {
                netIncome = String.Format("{0:0.00}", Convert.ToDouble(Math.Round((Math.Round(Convert.ToDouble(employee.Salary), 2) - (Math.Round(Convert.ToDouble(employee.Salary) / 22, 2) * Convert.ToDouble(input.RateMultiply))) - (Math.Round(Convert.ToDouble(employee.Salary) / 100, 2) * employee.Tax), 2)));
            } 
            else if (employee.IsDaily == true)
            {
                netIncome = String.Format("{0:0.00}", Convert.ToDouble(Math.Round((Math.Round(Convert.ToDouble(employee.Salary), 2) * Math.Round(Convert.ToDouble(input.RateMultiply), 2)) - (Math.Round(Convert.ToDouble(employee.Salary) / 100, 2) * employee.Tax), 2)));
            } 
            else if (employee.IsHourly == true)
            {
                netIncome = String.Format("{0:0.00}", Convert.ToDouble(Math.Round((Math.Round(Convert.ToDouble(employee.Salary), 2) * Math.Round(Convert.ToDouble(input.RateMultiply), 2)) - (Math.Round(Convert.ToDouble(employee.Salary) / 100, 2) * employee.Tax), 2)));
            }

            input.NetIncome = netIncome;
            return Ok(netIncome);
        }

    }
}
