using Api.Models;
using Domain.Logic.Infrastructure;
using Domain.Logic.Provision;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Logic
{
    public class MediaStrategyProvider : IMediaStrategyProvider
    {
        private readonly IDictionary<ProductMedia, IMediaStrategy> _strategies = new Dictionary<ProductMedia, IMediaStrategy>();

        public MediaStrategyProvider(IEnumerable<IMediaStrategy> strategies)
        {
            if (strategies == null)
            {
                throw new ArgumentNullException(nameof(strategies));
            }

            foreach (var strategy in strategies)
            {
                foreach (var supportedType in strategy.supportedProductTypes)
                {
                    _strategies.Add(supportedType, strategy);
                }
            }
        }

        public IMediaStrategy GetStrategy(ProductMedia productType)
        {
            // for future implementation should we decide to change to a class type instead of Enum
            if (!_strategies.TryGetValue(productType, out var strategy))
            {
                throw new NotSupportedException($"Type {productType.GetType()} is not supported.");
            }

            return strategy;
        }
    }
}
