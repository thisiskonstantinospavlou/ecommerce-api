using Api.Models;

namespace Domain.Abstractions
{
    public interface IValidator
    {
        void ValidateMembership(Membership customerMembership, List<Product> productsInOrder);
    }
}
