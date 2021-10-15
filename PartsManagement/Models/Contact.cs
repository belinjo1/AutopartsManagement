using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace PartsManagement.Models
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }
        public string Emri { get; set; }
        public string Mbiemri { get; set; }
        public string Pyetja { get; set; }
        public string Email { get; set; }
        public string CreatedAt { get; set; } = DateTime.Now.ToString("MM/dd/yyyy");
    }
}
