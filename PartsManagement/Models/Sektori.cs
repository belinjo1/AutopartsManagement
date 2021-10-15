using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartsManagement.Models
{
    public class Sektori
    {
        public int SektoriId { get; set; }
        public string Emri { get; set; }
        public string UserId {get; set;}
        public virtual ICollection<Produkti> Produktet { get; set; }
    }
}
