using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PartsManagement.Dtos;
namespace PartsManagement.Dtos
{
    public class CreateKomentiDTO
    {
        [Required]
        [MinLength(3,ErrorMessage ="Titulli nuk mund të jetë më i shkurtë se 3 karaktere")]
        public string Titulli { get; set; }

        [Required]
        [MinLength(15,ErrorMessage ="Mesazhi nuk mund të jetë më i shkurtë se 15 karaktere")]
        public string Body { get; set; }
        public string emriKomentuesit { get; set; }
        public string PuntoriId { get; set; }
        public string UserId { get; set; }
    }

    public class UpdateKomentiDTO : CreateKomentiDTO
    {
        
    }

    public class KomentiDTO : CreateKomentiDTO
    {
        public int KomentiId { get; set; }

    }

}