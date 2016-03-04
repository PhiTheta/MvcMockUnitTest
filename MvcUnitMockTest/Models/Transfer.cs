using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcUnitMockTest.Models
{
    public class Transfer
    {
        [Key]
        public int Id { get; set; }
        public int IdFrom { get; set; }
        public int IdTo { get; set; }
        public double Sum { get; set; }
        public DateTime Time { get; set; }
    }
}