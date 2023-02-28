using System.ComponentModel.DataAnnotations;
using System;

namespace AG.Models
{
    public class Notification
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public int PageNumper { get; set; }
        public int HasDiseaseID { get; set; }
        public HasDisease HasDisease { get; set; }
    }
}
