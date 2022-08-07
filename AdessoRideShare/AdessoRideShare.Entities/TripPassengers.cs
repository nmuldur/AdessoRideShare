using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdessoRideShare.Entities
{
    public class TripPassengers
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int TripID { get; set; }
        public string PassengerInfo { get; set; }
    }
}
