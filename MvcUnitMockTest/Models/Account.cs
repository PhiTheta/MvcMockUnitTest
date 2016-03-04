using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcUnitMockTest.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string Number { get; set; }
        public string Name {get;set;}
        public bool Locked { get; set; }
        public string Type { get; set; }
        public double Sum { get; set; }
    }
}