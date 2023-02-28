using AG.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AG.DTO
{
    public class LocationDTO
    {
        public int Id { get; set; }
      
        public List<Point> Points { get; set; }

    }
}
