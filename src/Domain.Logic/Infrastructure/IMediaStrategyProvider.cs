using Api.Models;
using Domain.Logic.Provision;

namespace Domain.Logic.Infrastructure
{
    public interface IMediaStrategyProvider
    {
        public IMediaStrategy GetStrategy(ProductMedia productMedia);
    }
}
