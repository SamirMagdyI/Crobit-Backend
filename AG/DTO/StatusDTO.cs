using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace AG.DTO
{
    public class StatusDTO
    {

       
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public int Humidity { get; set; }
        public int WaterLevel { get; set; }
        public int Salts { get; set; }
        public int Ph { get; set; }
        public int Heat { get; set; }
     

    }
}
