using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.DataTransferObjects
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Birthdate { get; set; }
        public string Tin { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public double Salary{ get; set; }
        public double Tax { get; set; }
        public bool IsMonthly { get; set; }
        public bool IsDaily { get; set; }
        public bool IsHourly { get; set; }
        public int IsDeleted { get; set; }
    }

}
