namespace Ewenze.Application.Services.ListingTypes
{
    public class ListingTypeConverter : IListingTypeConverter
    {
        public IList<ListingType.Models.ListingType> Convert(IEnumerable<Domain.Entities.ListingType> entities)
        {
            return entities.Select(entities => Convert(entities)).ToList();
        }

        public ListingType.Models.ListingType Convert(Domain.Entities.ListingType entity)
        {
            if (entity == null)
                return null;

            return new ListingType.Models.ListingType
            {
                Id = entity.Id,
                Title = entity.Title,
                Status = entity.Status,
                CreationDate = entity.CreationDate,
                ModifiedDate = entity.ModifiedDate
            };
        }
    }
}
