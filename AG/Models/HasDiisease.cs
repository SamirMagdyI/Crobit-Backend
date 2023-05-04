using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AG.Models
{
    public class HasDisease
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int photoId { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("Diseases")]
        [Required]
        public int DiseasesID { get; set; }
        

        public Photo PlantPhoto { get; set; }
        
        public Diseases diseases { get; set; }
      //  public Notification Notification { get; set; }


    }
}
