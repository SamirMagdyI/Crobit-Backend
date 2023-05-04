using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AG.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string photo { get; set; }

        [ForeignKey("User")]
        [Required]
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public double Lan { get; set; }
        public double Long { get; set; }


    }
}
