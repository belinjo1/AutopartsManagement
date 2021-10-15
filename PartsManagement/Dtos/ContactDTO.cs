using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PartsManagement.Dtos
{
    public class CreateContactDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Emri duhet të ketë së paku 3 shkronja")]
        public string Emri { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Mbiemri duhet të ketë së paku 3 shkronja")]
        public string Mbiemri { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Pyetja duhet të ketë së paku 3 shkronja")]
        public string Pyetja { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [MinLength(3, ErrorMessage = "Emri duhet të ketë së paku 3 shkronja")]
        public string Email { get; set; }
    }
    public class UpdateContactDTO : CreateContactDTO { }

    public class ContactDTO : CreateContactDTO
    {
        public int ContactId { get; set; }
        public string CreatedAt { get; set; }
    }
}




 

