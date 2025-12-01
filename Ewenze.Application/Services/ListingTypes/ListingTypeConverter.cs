namespace Ewenze.Application.Services.ListingTypes
{
    public class ListingTypeConverter : IListingTypeConverter
    {
        public IList<ListingType.Models.ListingTypeApplicationModel> Convert(IEnumerable<Domain.Entities.ListingTypeV2> entities)
        {
            return entities.Select(entities => Convert(entities)).ToList();
        }

        public ListingType.Models.ListingTypeApplicationModel Convert(Domain.Entities.ListingTypeV2 entity)
        {
            if (entity == null)
                return null;

            return new ListingType.Models.ListingTypeApplicationModel
            {
                Id = entity.Id,
                Title = entity.Label,
                Icon = entity.Icon,
                CreationDate = entity.CreatedAt.UtcDateTime,
                ModifiedDate = entity.UpdatedAt.UtcDateTime
            };
        }
    }
}
