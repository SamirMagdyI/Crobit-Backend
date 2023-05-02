using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AG.Models
{
    public class Status
    {
        [Key]
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public int Humidity { get; set; }
        public int WaterLevel { get; set; }
        public int Salts { get; set; }
        public int Ph { get; set; }
        
        
        public int Heat { get; set; }
        [ForeignKey("User")]
        [Required]
        public string UserId { get; set; }
        public AppUser User { get; set; }
        
    }
}
