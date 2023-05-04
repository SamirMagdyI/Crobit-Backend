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
        public double Humidity { get; set; }
        public double WaterLevel { get; set; }
        public double N { get; set; }
        public double P { get; set; }
        public double K { get; set; }


        public double Ph { get; set; }
        public double Lan { get; set; }
        public double Long { get; set; }
        
        
        public double Heat { get; set; }
        [ForeignKey("User")]
        [Required]
        public string UserId { get; set; }
        public AppUser User { get; set; }
        
    }
}
