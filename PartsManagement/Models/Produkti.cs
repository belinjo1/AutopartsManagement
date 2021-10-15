using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace PartsManagement.Models
{
    public class Produkti
    {
        public int ProduktiId { get; set; }
        public string Emri { get; set; }
        public string Number { get; set; }
        public int Sasia { get; set; }

        public int SektoriId { get; set; }

        public Sektori Sektori { get; set; }
        public virtual ICollection<FaturaIN> Faturat { get; set; }
        public virtual ICollection<FaturaOUT> FaturatOUT { get; set; }
        public int MarkaId { get;set; }
        public Marka Marka { get; set; }

    }
}
