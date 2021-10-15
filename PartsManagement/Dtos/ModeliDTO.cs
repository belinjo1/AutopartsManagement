using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PartsManagement.Dtos;
namespace PartsManagement.Dtos
{
    public class CreateModeliDTO
    {
        [Required]
        [MinLength(3,ErrorMessage ="Emri nuk mund të jetë më i shkurtë se 3 karaktere")]
        public string Emri { get; set; }
        public int MarkaId { get; set; }
    }

    public class UpdateModeliDTO : CreateModeliDTO
    {
        
    }

    public class ModeliDTO : CreateModeliDTO
    {
        public int ModelId { get; set; }
    }
}