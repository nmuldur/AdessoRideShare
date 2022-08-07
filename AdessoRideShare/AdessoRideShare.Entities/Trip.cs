using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdessoRideShare.Entities
{
    public class Trip
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Driver")]
        public int DriverID { get; set; }
        public virtual TripDriver Driver { get; set; }

        [MaxLength(500)]
        public string FromAddress { get; set; }
        [MaxLength(500)]
        public string ToAddress { get; set; }
        public DateTime TripDate { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public int SeatCapacity { get; set; }
        public bool IsLive { get; set; }
    }
}