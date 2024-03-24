using Api.Models;
using Domain.Abstractions;
using Domain.Abstractions.Exceptions;

namespace Domain.Logic
{
    public class Validator : IValidator
    {
        public void ValidateMembership(Membership customerMembership, List<Product> productsInOrder)
        {
            productsInOrder.ForEach(item =>
            {
                if (item.Type == ProductType.Book)
                {
                    var hasMembershipInOrder = productsInOrder.Any(i => i.Type == ProductType.Membership && (i.Name.ToLower() == "book" || i.Name.ToLower() == "premium"));
                    if (!hasMembershipInOrder)
                    {
                        var hasMembershipInAccount = (customerMembership == Membership.Book || customerMembership == Membership.Premium);
                        if (!hasMembershipInAccount)
                        {
                            throw new ValidMembershipNotFoundException($"The order contains products without a valid membership in customer account or order.");
                        }
                    }
                }

                if (item.Type == ProductType.Video)
                {
                    var hasMembershipInOrder = productsInOrder.Any(i => i.Type == ProductType.Membership && (i.Name.ToLower() == "video" || i.Name.ToLower() == "premium"));
                    if (!hasMembershipInOrder)
                    {
                        var hasMembershipInAccount = (customerMembership == Membership.Video || customerMembership == Membership.Premium);
                        if (!hasMembershipInAccount)
                        {
                            throw new ValidMembershipNotFoundException($"The order contains products without a valid membership in customer account or order.");
                        }
                    }
                }
            });
        }
    }
}
