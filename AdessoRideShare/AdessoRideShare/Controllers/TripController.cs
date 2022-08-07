using AdessoRideShare.Business.Abstract;
using AdessoRideShare.Entities;
using AdessoRideShare.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdessoRideShare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : Controller
    {
        private ITripService _tripService;
        public TripController(ITripService tripService)
        {
            _tripService = tripService;
        }
        // GET: api/tripList
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var tripList = await _tripService.GetAsync();
                if (tripList == null)
                    return NotFound();

                return Ok(tripList);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        // GET: api/tripList/5
        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var trip = await _tripService.GetByIdAsync(id);
                if (trip == null)
                    return NotFound();

                return Ok(trip);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        // POST: api/tripList
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Trip tripModel)
        {
            try
            {
                if (tripModel.ID > 0)
                {
                    var result = await _tripService.UpdateAsync(tripModel.ID, tripModel);
                    if (result.Status)
                        return Ok();
                    else
                        return NotFound();
                }
                else
                {
                    var tripID = await _tripService.AddAsync(tripModel);
                    if (tripID > 0)
                        return Ok(tripID);
                    else
                        return BadRequest("Seyahat oluşturulurken sorun oluştu.");

                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("changeLiveStatu")]
        public async Task<IActionResult> Post([FromBody] ChangeLiveStatuRequest changeLiveStatuRequestModel)
        {
            try
            {
                if (changeLiveStatuRequestModel.TripID > 0)
                {
                    Trip trip = await _tripService.GetByIdAsync(changeLiveStatuRequestModel.TripID);
                    trip.IsLive = changeLiveStatuRequestModel.IsLive;
                    var result = await _tripService.UpdateAsync(changeLiveStatuRequestModel.TripID, trip);
                    if (result.Status)
                        return Ok("Başarılı.");
                    else
                        return NotFound();
                }
                else
                {
                    return BadRequest("Hatalı seyahat numarası.");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("searchTrip")]
        public async Task<IActionResult> Post([FromBody] SearchTripRequest searchTripRequest)
        {
            try
            {
                if (searchTripRequest == null || string.IsNullOrWhiteSpace(searchTripRequest.FromAddress) || string.IsNullOrWhiteSpace(searchTripRequest.ToAddress))
                {
                    return BadRequest("Hatalı arama.");

                }
                else
                {
                    List<Trip> tripList = await _tripService.SearchTripByAddress(searchTripRequest);
                    if (tripList == null)
                        return NotFound();
                    
                    return Ok(tripList);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
