using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartsManagement.Models
{
    public class User : IdentityUser
    {   
        public string Emri { get; set; }
        public string Mbiemri { get; set; }
        public string Kompania { get; set; }
        public int QytetiId {get;set;}
        public Qyteti Qyteti { get; set; }
        
        public string ShefiId { get; set; }
        public User Shefi { get; set; }
        public  ICollection<Sektori> Sektoret { get; set; }
        public ICollection<User> Puntoret { get; set; }
        public  ICollection<Furnitori> Furnitoret { get; set; }
        public  ICollection<Porosia> Porosite { get; set; }
        public  ICollection<Komenti> Komentet { get; set; }


    }
}

