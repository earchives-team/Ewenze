namespace Ewenze.Application.Services.ListingTypes
{
    public interface IListingTypeConverter
    {
        IList<ListingType.Models.ListingType> Convert(IEnumerable<Domain.Entities.ListingType> entities);
        ListingType.Models.ListingType Convert(Domain.Entities.ListingType entity);
    }
}
