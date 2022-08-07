using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdessoRideShare.Entities
{
    public class TripDriver
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(500)]
        public string NameSurname { get; set; }
    }
}
