using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PartsManagement.Dtos
{
    public class LoginDTO
    {
        //[Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //[Required]
        [StringLength(15, ErrorMessage = "Fjalëkalimi duhet të ketë së paku 8 shkronja", MinimumLength = 8)]
        public string Password { get; set; }
    }
        public class UserDTO : LoginDTO
    {
        public string Emri { get; set; }
        public string Mbiemri { get; set; }
        public string Kompania { get; set; }
        public int QytetiId { get; set; }
        public string ShefiId { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}