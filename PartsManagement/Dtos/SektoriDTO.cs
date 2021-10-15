using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PartsManagement.Dtos;

namespace PartsManagement.Dtos
{
    public class CreateSektoriDTO
    {
        [Required]
        [MinLength(3,ErrorMessage ="Emri i sektorit duhet të ketë së paku 3 shkronja")]
        public string Emri { get; set; }

        //[Required]
        public string UserId { get; set; }

    }

    public class UpdateSektoriDTO : CreateSektoriDTO
    {
        
    }

    public class SektoriDTO : CreateSektoriDTO
    {
        public int SektoriId { get; set; }
    }
}