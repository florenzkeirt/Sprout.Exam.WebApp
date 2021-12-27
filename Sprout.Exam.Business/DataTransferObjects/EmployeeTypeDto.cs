using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.DataTransferObjects
{
    public class EmployeeTypeDto
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public double Tax { get; set; }
        public double Salary { get; set; }
        public bool IsMonthly { get; set; }
        public bool IsDaily { get; set; }
        public bool IsHourly { get; set; }
    }

}
