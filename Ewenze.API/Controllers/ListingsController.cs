using Ewenze.Application.Services.Listings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ewenze.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingsController : ControllerBase
    {
        private readonly IListingService ListingService; 
        private readonly Converters.ListingConverter ListingConverter;

        public ListingsController(IListingService listingService, Converters.ListingConverter listingConverter)
        {
            this.ListingService = listingService ?? throw new ArgumentNullException(nameof(listingService));
            this.ListingConverter = listingConverter ?? throw new ArgumentNullException(nameof(listingConverter));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Models.ListingDto.ListingOutputDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var serviceModels = await ListingService.GetAllAsync();
            var outPutDtos = ListingConverter.Convert(serviceModels);
            return Ok(outPutDtos);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Models.ListingDto.ListingOutputDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var serviceModel = await ListingService.GetByIdAsync(id);
            var outPutDtos = ListingConverter.Convert(serviceModel);
            return Ok(outPutDtos);
        }
    }
}
