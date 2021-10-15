using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartsManagement.Models
{
    public class Furnitori
    {   
        public int FurnitoriId { get; set; }
        public string Emri { get; set; }
        public string Mbiemri { get; set; }
        public string Lokacioni { get; set; }
        public string Telefoni { get; set;}

        public string UserId {get; set;}
        public User User { get; set; } 
    }
}
