using System.Collections.Generic;

namespace PartsManagement.Models
{
    public class Shteti
    {
        public int ShtetiId { get; set; }
        public string Emri { get; set; }

        public ICollection<Qyteti> Qyteti { get; set; }
    }
}
