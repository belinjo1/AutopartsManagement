using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartsManagement.Models
{
    public class Komenti
    {
        public int KomentiId { get; set; }
        public string Titulli { get; set; }
        public string Body { get; set; }
        public string emriKomentuesit { get; set; }
        public string UserId {get; set;}
        public string PuntoriId { get; set; }
        public User User { get; set; }

    }
}
