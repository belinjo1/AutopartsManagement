using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace PartsManagement.Models
{
    public class FaturaIN
    {
        [Key]
        public int FaturaId { get; set; }
        public int Sasia { get; set; }
        public double Qmimi { get; set; }
        public string CreatedAt { get; set; } = DateTime.Now.ToString("MM/dd/yyyy");
        public int ProduktiId { get; set; }
        public string UserId { get; set; }

        [JsonIgnore]
        public Produkti Produkti { get; set; }
    }
}
