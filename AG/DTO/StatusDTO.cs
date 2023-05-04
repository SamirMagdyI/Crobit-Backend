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
        public double Humidity { get; set; }
        public double WaterLevel { get; set; }
        public double N { get; set; }
        public double P { get; set; }
        public double K { get; set; }


        public double Ph { get; set; }
        public double Heat { get; set; }
        public double Lan { get; set; }
        public double Long { get; set; }


    }
}
