using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PartsManagement.Dtos;
namespace PartsManagement.Dtos
{
    public class CreateProduktiDTO
    {
        [Required]
        [MinLength(3,ErrorMessage ="Emri nuk mund të jetë më i shkurtë se 3 karaktere")]
        public string Emri { get; set; }


        [Required]
        [MinLength(4,ErrorMessage ="Numri nuk mund të jetë më i shkurtë se 6 karaktere")]
        public string Number { get; set; }

        public int SektoriId { get; set; }

        public int MarkaId { get; set; }

    }

    public class UpdateProduktiDTO : CreateProduktiDTO
    {
        
    }

    public class ProduktiDTO : CreateProduktiDTO
    {
        public int ProduktiId { get; set; }
        public int Sasia { get; set; }
    }
}