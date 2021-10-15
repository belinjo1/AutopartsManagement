using System.ComponentModel.DataAnnotations.Schema;

namespace PartsManagement.Models
{
    public class Porosia
    {
        public int PorosiaId { get; set; }
        public string Titulli { get; set; }
        public int Sasia { get; set; }
        public string Klienti { get; set; }
        public string Telefoni { get; set; }
        public string UserId {get; set;}
        public User User { get; set; }

    }
}
