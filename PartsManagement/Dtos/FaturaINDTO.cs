using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PartsManagement.Dtos
{
    public class CreateFaturaINDTO
    {

        [Required]
        [Range(1, 555, ErrorMessage = "Sasia nuk mund të jetë 0")]
        public int Sasia { get; set; }

        [Required]
        public double Qmimi { get; set; }

        public int ProduktiId { get; set; }

        public string UserId { get; set; }

    }

    public class UpdateFaturaINDTO : CreateFaturaINDTO { }

    public class FaturaINDTO : CreateFaturaINDTO
        {
            public int FaturaId { get; set; }
            public string CreatedAt { get; set; }
    }
    
}
