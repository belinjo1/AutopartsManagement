using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartsManagement.Models
{
    public class Qyteti
    {
        public int QytetiId { get; set; }
        public string Emri { get; set; }

        public int ShtetiId { get; set; }
        public Shteti Shteti { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
