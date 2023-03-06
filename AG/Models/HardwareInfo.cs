using AG.Migrations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AG.Models
{
    public class HardwareInfo
    {
        [Key]
       public int Id { get; set; }
        [Required]
       public string HardwareNum { get; set; }
        [ForeignKey("User")]
        [Required]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
