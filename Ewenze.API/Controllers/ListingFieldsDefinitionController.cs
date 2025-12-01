using Ewenze.Application.Services.ListingFieldDefition;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ewenze.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingFieldsDefinitionController : ControllerBase
    {
        private readonly IListingFieldDefinitionService ListingFieldDefinitionService;
        public ListingFieldsDefinitionController(IListingFieldDefinitionService listingFieldDefinitionService)
        {
            this.ListingFieldDefinitionService = listingFieldDefinitionService ?? throw new ArgumentNullException(nameof(listingFieldDefinitionService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var listingFieldDefinitions = await ListingFieldDefinitionService.GetAllAsync();
            return Ok(listingFieldDefinitions);
        }
    }
}
