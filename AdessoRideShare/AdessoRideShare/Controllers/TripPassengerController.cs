using AdessoRideShare.Business.Abstract;
using AdessoRideShare.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AdessoRideShare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripPassengerController : Controller
    {
        private ITripPassengersService _tripPassengerService;
        private ITripService _tripService;
        public TripPassengerController(ITripPassengersService tripPassengerService, ITripService tripService)
        {
            _tripPassengerService = tripPassengerService;
            _tripService = tripService;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TripPassengers tripPassengerModel)
        {
            try
            {
                List<TripPassengers> passengers = await _tripPassengerService.FindPassengersByTripIDAsync(tripPassengerModel.TripID);
                Trip trip = await _tripService.GetByIdAsync(tripPassengerModel.TripID);
                if (trip == null)
                {
                    return BadRequest("Seyahat bulunamamıştır.");
                }
                if (passengers.Count == trip.SeatCapacity)
                {
                    return BadRequest("Seyahatin kapasitesi dolmuştur.");
                }
                else
                {
                    var tripPassengerID = await _tripPassengerService.AddAsync(tripPassengerModel);
                    if (tripPassengerID > 0)
                        return Ok(tripPassengerID);
                    else
                        return BadRequest("Seyahata yolcu eklenirken sorun oluştu.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
