using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartsManagement.Models
{
    public class Marka
    {
        public int MarkaId { get; set; }

        public string Emri { get; set; }

        public ICollection<Modeli> Modelet { get; set; }

    }
}
