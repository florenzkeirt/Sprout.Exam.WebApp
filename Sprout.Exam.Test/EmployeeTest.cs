using Sprout.Exam.DataAccess;
using System;
using System.Data;
using Xunit;

namespace Sprout.Exam.Test
{
    [Collection("Add, edit, and update employee test cases")]
    public class AddEditUpdateEmployeeTest
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData(56, null)]
        [InlineData(null, false)]
        public void GetEmployee_ShouldWork(int? empID, bool? isDeleted)
        {
            EmployeeAccess employeeAccess = new EmployeeAccess();
            DataTable employee = new DataTable();

            employee = employeeAccess.GetEmployee(empID, isDeleted);

            Assert.True(employee.Rows.Count > 0);
        }

        [Theory]
        [InlineData(null, "Test 1", "2021-04-12", "654654", 1, null)]
        [InlineData(null, "Test 2", "1945-05-25", "123142", 2, null)]
        [InlineData(null, "Test 3", "1953-10-11", "798798", 3, null)]
        public void AddEmployee_ShouldWork(int? empID, string fullName, string birthDate, string tin, int typeId, int? isDeleted)
        {
            EmployeeAccess employeeAccess = new EmployeeAccess();
            DataTable employee = new DataTable();

            employee = employeeAccess.AddUpdateDeleteEmployee(empID, fullName, birthDate, tin, typeId, isDeleted);

            Assert.True(employee.Rows.Count > 0);
        }

        [Theory]
        [InlineData(1, "John Doe", "1995-04-25", "123456796", 1, 0)]
        [InlineData(1, "Test 2", "1956-02-13", "31264", 3, 0)]
        [InlineData(1, "Test 1", "1975-10-23", "789797", 2, 0)]
        public void EditEmployee_ShouldWork(int empID, string fullName, string birthDate, string tin, int typeId, int isDeleted)
        {
            EmployeeAccess employeeAccess = new EmployeeAccess();
            DataTable employee = new DataTable();
            int matchCount = 0;

            employee = employeeAccess.AddUpdateDeleteEmployee(empID, fullName, birthDate, tin, typeId, isDeleted);

            foreach (DataRow row in employee.Rows)
            {
                foreach (DataColumn column in employee.Columns)
                {
                    switch (column.ColumnName)
                    {
                        case "Birthdate":
                            if (string.Format("{0:yyyy-MM-dd}", row[column]) == birthDate) matchCount++;
                            break;
                        case "FullName":
                            if (row[column].ToString() == fullName) matchCount++;
                            break;
                        case "Id":
                            if (Convert.ToInt32(row[column]) == empID) matchCount++;
                            break;
                        case "TIN":
                            if (row[column].ToString() == tin) matchCount++;
                            break;
                        case "EmployeeTypeId":
                            if (Convert.ToInt32(row[column]) == typeId) matchCount++;
                            break;
                    }
                }
            }

            Assert.True(matchCount == 5);
        }
    }
}
