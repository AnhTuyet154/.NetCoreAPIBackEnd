using Microsoft.AspNetCore.Mvc;
using WebAPIServices.Models.DTO;
using WebAPIServices.Services.SuperHeroService;

namespace WebAPIServices.Controllers
{
    [Route("api/Seller")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly ISellerService _sellerService;

        public SellerController(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SellerDto>>> GetAllSellers()
        {
            var sellers = await _sellerService.GetAllSellersAsync();
            return Ok(sellers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SellerDto>> GetSingleSeller(int id)
        {
            var seller = await _sellerService.GetSingleSellerAsync(id);
            if (seller == null)
            {
                return NotFound("Seller not found");
            }
            return Ok(seller);
        }

        [HttpPost]
        public async Task<ActionResult<SellerDto>> AddSeller(SellerDto sellerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addedSeller = await _sellerService.AddSellerAsync(sellerDto);
            return Ok(addedSeller);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SellerDto>> UpdateSeller(int id, SellerDto sellerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedSeller = await _sellerService.UpdateSellerAsync(id, sellerDto);
            if (updatedSeller == null)
            {
                return NotFound("Seller not found");
            }

            return Ok(updatedSeller);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SellerDto>>> DeleteSeller(int id)
        {
            var deletedSellers = await _sellerService.DeleteSellerAsync(id);
            if (deletedSellers == null)
            {
                return NotFound("Seller not found");
            }
            return Ok(deletedSellers);
        }
    }
}
