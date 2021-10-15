using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PartsManagement.Dtos;

namespace PartsManagement.Dtos
{
    public class CreateFurnitoriDTO
    {
        [Required]
        [MinLength(3,ErrorMessage ="Emri i furnitorit duhet të ketë së paku 3 shkronja")]
        public string Emri { get; set; }

        [Required]
        [MinLength(3,ErrorMessage ="Mbiemri i furnitorit duhet të ketë së paku 3 shkronja")]
        public string Mbiemri { get; set; }

        [Required]
        [MinLength(3,ErrorMessage ="Lokacioni duhet të ketë së paku 3 shkronja")]
        public string Lokacioni { get; set; }

        [Required]
        [MinLength(3,ErrorMessage ="Numri i telefonit jo valid")]
        public string Telefoni { get; set;}

        public string UserId { get; set; }

    }

    public class UpdateFurnitoriDTO : CreateFurnitoriDTO
    {
        
    }

    public class FurnitoriDTO : CreateFurnitoriDTO
    {
        public int FurnitoriId { get; set; }

    }
}