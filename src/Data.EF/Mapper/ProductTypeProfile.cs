using AutoMapper;
using Domain.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Data.EF.Mapper
{
    public class ProductTypeProfile : Profile
    {
        public ProductTypeProfile()
        {
            CreateMap<Data.EF.Models.Product, Api.Models.ProductType>()
                .ConvertUsing(s => MapProductType(s.TypeId));
        }

        public static Api.Models.ProductType MapProductType(int typeId)
        {
            switch (typeId)
            {
                case 1:
                    return Api.Models.ProductType.Membership;
                case 2:
                    return Api.Models.ProductType.Video;
                case 3:
                    return Api.Models.ProductType.Book;
                default:
                    throw new InternalServerErrorException();
            }
        }
    }
}
