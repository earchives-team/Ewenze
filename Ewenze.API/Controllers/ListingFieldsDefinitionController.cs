using Ewenze.API.Models.ListingFieldsDefintions;
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
            var dto = listingFieldDefinitions.Select(listingFieldDefinition => new ListingFieldDefinitionOutputDto()
            {
                Id = listingFieldDefinition.Id,
                ListingTypeId = listingFieldDefinition.ListingTypeId,
                Name = listingFieldDefinition.Name,
                Description = listingFieldDefinition.Description,
                Schema = listingFieldDefinition.Schema,
                Version = listingFieldDefinition.Version,
                CreatedAt = listingFieldDefinition.CreatedAt,
                UpdatedAt = listingFieldDefinition.UpdatedAt
            });

            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var listingFieldDefinition = await ListingFieldDefinitionService.GetByIdAsync(id);

            var dto = new ListingFieldDefinitionOutputDto()
            {
                Id = listingFieldDefinition.Id,
                ListingTypeId = listingFieldDefinition.ListingTypeId,
                Name = listingFieldDefinition.Name,
                Description = listingFieldDefinition.Description,
                Schema = listingFieldDefinition.Schema,
                Version = listingFieldDefinition.Version,
                CreatedAt = listingFieldDefinition.CreatedAt,
                UpdatedAt = listingFieldDefinition.UpdatedAt
            };
            return Ok(dto);
        }
    }
}
