using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AG.Models
{
    public class Location
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        [Required]
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public List<Point> Points { get; set; }
        
    }

    

    public class Point
    {
        [Key]
        public int Id { get; set; }
        public double Lan { get; set; }
        public double Long { get; set; }
        [ForeignKey("Location")]
        public int LocationID { get; set; }
        //  public Location Location { get; set; }

    }
}
