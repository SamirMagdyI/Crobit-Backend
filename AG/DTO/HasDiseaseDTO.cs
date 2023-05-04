using AG.Models;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace AG.DTO
{
    public class HasDiseaseDTO
    {
        [Key]
        public int Id { get; set; }
     
        [Required]
        public int photoId { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("Diseases")]
        [Required]
        public int DiseasesID { get; set; }



        //public string PhotoPath { get; set; }

        //public StatusDTO Status { get; set; }
        //public Diseases diseases { get; set; }

    }

    public class HasDiseaseResponse
    {
        public PhotoDto Photo { get; set; }
        public Diseases Diseases { get; set; }
    }
}
