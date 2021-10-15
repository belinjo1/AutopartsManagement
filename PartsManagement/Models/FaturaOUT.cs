using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartsManagement.Models
{
    public class FaturaOUT
    {
        [Key]
        public int FaturaId { get; set; }
        public int Sasia { get; set; }
        public double Qmimi { get; set; }
        public double Totali { get; set; }
        public string Shitesi { get; set; }
        public int ProduktiId { get; set; }
        public string UserId { get; set; }
        public string CreatedAt { get; set; } = DateTime.Now.ToString("MM/dd/yyyy");

        [JsonIgnore]
        public Produkti Produkti { get; set; }
        public User User { get; set; }
    }
}
