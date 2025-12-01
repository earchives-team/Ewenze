namespace Ewenze.Application.Services.ListingTypes
{
    public interface IListingTypeConverter
    {
        IList<ListingType.Models.ListingTypeApplicationModel> Convert(IEnumerable<Domain.Entities.ListingTypeV2> entities);
        ListingType.Models.ListingTypeApplicationModel Convert(Domain.Entities.ListingTypeV2 entity);
    }
}
