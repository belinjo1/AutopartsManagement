using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PartsManagement.Models
{
    public class Modeli
    {
        [Key]
        public int ModelId { get; set; }
        public string Emri { get; set; }
        public int MarkaId { get; set; }
        public Marka Marka { get; set; }
 
    }
}
