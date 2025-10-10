using Ewenze.API.Models.ListingTypeDto;
using Ewenze.Application.Services.ListingTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ewenze.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingTypesController : ControllerBase
    {
        private readonly IListingTypeService ListingTypeService;
        private readonly Converters.ListingTypeConverter ListingTypeConverter;
        public ListingTypesController(IListingTypeService listingTypeService, Converters.ListingTypeConverter listingTypeConverter)
        {
            this.ListingTypeService = listingTypeService ?? throw new ArgumentNullException(nameof(listingTypeService));
            this.ListingTypeConverter = listingTypeConverter ?? throw new ArgumentNullException(nameof(listingTypeConverter));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ListingTypeOuputDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var serviceModels = await ListingTypeService.GetAllListingTypesAsync();
            var outPutDtos = ListingTypeConverter.Convert(serviceModels);

            return Ok(outPutDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var serviceModel = await ListingTypeService.GetListingTypeByIdAsync(id);
            var outPutDtos = ListingTypeConverter.Convert(serviceModel);
            return Ok(outPutDtos);
        }
    }
}
