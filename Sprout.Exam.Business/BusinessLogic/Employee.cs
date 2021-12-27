using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;

namespace Sprout.Exam.Business.BusinessLogic
{
    public class Employee
    {
        /// <summary>
        /// Gets employee records based on the given parameters
        /// </summary>
        /// <param name="empID">employee ID</param>
        /// <param name="isDeleted">delete status</param>
        /// <returns>returns a list containing the record(s) of specified employee</returns>
        public static List<EmployeeDto> GetEmployee(int? empID, bool? isDeleted)
        {
            DataTable dataEmployee = new DataTable();
            List<EmployeeDto> employees = new List<EmployeeDto>();
            EmployeeAccess employeeAccess = new EmployeeAccess();

            try
            {
                dataEmployee = employeeAccess.GetEmployee(empID, isDeleted);
                foreach (DataRow dr in dataEmployee.Rows)
                {
                    employees.Add(
                        new EmployeeDto
                        {
                            Birthdate = string.Format("{0:yyyy-MM-dd}", dr["Birthdate"]),
                            FullName = dr["FullName"].ToString(),
                            Id = Convert.ToInt32(dr["Id"]),
                            Tin = dr["TIN"].ToString(),
                            TypeId = Convert.ToInt32(dr["EmployeeTypeId"]),
                            TypeName = dr["TypeName"].ToString(),
                            Salary = Math.Round(Convert.ToDouble(dr["Salary"]), 2),
                            Tax = Math.Round(Convert.ToDouble(dr["Tax"]), 2),
                            IsMonthly = Convert.ToInt32(dr["IsMonthly"]) == 1 ? true : false,
                            IsDaily = Convert.ToInt32(dr["IsDaily"]) == 1 ? true : false,
                            IsHourly = Convert.ToInt32(dr["IsHourly"]) == 1 ? true : false,
                            IsDeleted = Convert.ToInt32(dr["IsDeleted"])
                        });
                }

                return employees;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Gets employee types  based on the given parameters
        /// </summary>
        /// <param name="typeID">employee type ID</param>
        /// <returns>returns a list containig the records of the specified employee type</returns>
        public static List<EmployeeTypeDto> GetEmployeeType(int? typeID)
        {
            DataTable dataEmployee = new DataTable();
            List<EmployeeTypeDto> employeeTypes = new List<EmployeeTypeDto>();
            EmployeeAccess employeeAccess = new EmployeeAccess();

            try
            {
                dataEmployee = employeeAccess.GetEmployeeType(typeID);
                foreach (DataRow dr in dataEmployee.Rows)
                {
                    employeeTypes.Add(
                        new EmployeeTypeDto
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            TypeName = dr["TypeName"].ToString(),
                            Salary = Math.Round(Convert.ToDouble(dr["Salary"]), 2),
                            IsMonthly = Convert.ToInt32(dr["IsMonthly"]) == 1 ? true : false,
                            IsDaily = Convert.ToInt32(dr["IsDaily"]) == 1 ? true : false,
                            IsHourly = Convert.ToInt32(dr["IsHourly"]) == 1 ? true : false,
                        });
                }

                return employeeTypes;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Gets employee records based on the given parameters
        /// </summary>
        /// <param name="employee">employee model</param>
        /// <returns>returns a list containing the record(s) of the added, updated, or deleted employee</returns>
        public static List<EmployeeDto> AddUpdateDeleteEmployee(AddUpdateDeleteDto employee)
        {
            DataTable dataEmployee = new DataTable();
            List<EmployeeDto> employees = new List<EmployeeDto>();
            EmployeeAccess employeeAccess = new EmployeeAccess();

            try
            {
                dataEmployee = employeeAccess.AddUpdateDeleteEmployee(employee.Id, employee.FullName, string.Format("{0:yyyy-MM-dd}", employee.Birthdate), employee.Tin, employee.TypeId, employee.IsDeleted);
                foreach (DataRow dr in dataEmployee.Rows)
                {
                    employees.Add(
                        new EmployeeDto
                        {
                            Birthdate = string.Format("{0:yyyy-MM-dd}", dr["Birthdate"]),
                            FullName = dr["FullName"].ToString(),
                            Id = Convert.ToInt32(dr["Id"]),
                            Tin = dr["TIN"].ToString(),
                            TypeId = Convert.ToInt32(dr["EmployeeTypeId"]),
                            TypeName = dr["TypeName"].ToString(),
                            Salary = Math.Round(Convert.ToDouble(dr["Salary"]), 2),
                            Tax = Math.Round(Convert.ToDouble(dr["Tax"]), 2),
                            IsMonthly = Convert.ToInt32(dr["IsMonthly"]) == 1 ? true : false,
                            IsDaily = Convert.ToInt32(dr["IsDaily"]) == 1 ? true : false,
                            IsHourly = Convert.ToInt32(dr["IsHourly"]) == 1 ? true : false,
                            IsDeleted = Convert.ToInt32(dr["IsDeleted"])
                        });
                }

                return employees;
            }
            catch
            {
                throw;
            }
        }
    }
}
