using System.ComponentModel.DataAnnotations;
namespace PartsManagement.Dtos
{
    public class CreateMarkaDTO
    {
        [Required]
        [MinLength(3,ErrorMessage ="Emri nuk mund të jetë më i shkurtë se 3 karaktere")]
        public string Emri { get; set; }
        
    }

    public class UpdateMarkaDTO : CreateMarkaDTO
    {
        
    }

    public class MarkaDTO : CreateMarkaDTO
    {
        public int MarkaId { get; set; }
    }
}