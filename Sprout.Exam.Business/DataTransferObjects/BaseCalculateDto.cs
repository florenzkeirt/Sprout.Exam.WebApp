using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.DataTransferObjects
{
    public abstract class BaseCalculateDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        public string Tin { get; set; }
        public int TypeId { get; set; }
        public decimal RateMultiply { get; set; }
        public string NetIncome { get; set; }
    }
}
