using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Data.EF.Mapper
{
    public class ProductMediaProfile : Profile
    {
        public ProductMediaProfile()
        {
            CreateMap<Data.EF.Models.Product, Api.Models.ProductMedia>()
                .ConstructUsing(s => MapProductMedia(s.IsPhysical, s.TypeId));
        }

        public static Api.Models.ProductMedia MapProductMedia(bool isPhysical, int typeId)
        {
            if (isPhysical)
            {
                return Api.Models.ProductMedia.Physical;
            }

            switch (typeId)
            {
                case 1:
                    return Api.Models.ProductMedia.Membership;
                default:
                    return Api.Models.ProductMedia.Electronic;
            }
        }
    }
}
