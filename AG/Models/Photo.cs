using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AG.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string photo { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }


    }
}
