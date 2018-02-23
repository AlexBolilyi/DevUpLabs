using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DevUpLabs.Models
{
    public class Employees
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string JobTitle { get; set; }

        [Required]
        public int Rate { get; set; }

        [Required]
        public DateTime EmploymentDate { get; set; }

    }

}