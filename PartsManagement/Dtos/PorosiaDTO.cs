using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PartsManagement.Dtos;
namespace PartsManagement.Dtos
{
    public class CreatePorosiaDTO
    {
        [Required]
        [MinLength(3,ErrorMessage ="Titulli nuk mund të jetë më i shkurtë se 3 karaktere")]
        public string Titulli { get; set; }
        
        [Required]
        public int Sasia { get; set; }

        [Required]
        [MinLength(6,ErrorMessage ="Klienti nuk mund të jetë më i shkurtë se 6 karaktere")]
        public string Klienti { get; set; }
        
        [Required]
        [MinLength(6,ErrorMessage ="Numri nuk mund të jetë më i shkurtë se 6 karaktere")]
        public string Telefoni { get; set; }
        public string UserId { get; set; }
    }

    public class UpdatePorosiaDTO : CreatePorosiaDTO
    {
        
    }

    public class PorosiaDTO : CreatePorosiaDTO
    {
        public int PorosiaId { get; set; }
    }
}