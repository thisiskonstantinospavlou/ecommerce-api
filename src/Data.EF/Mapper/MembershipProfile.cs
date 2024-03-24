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
    public class MemebershipProfile : Profile
    {
        public MemebershipProfile()
        {
            CreateMap<Data.EF.Models.Membership, Api.Models.Membership>()
                .ConvertUsing(s => MapProductType(s.MembershipId));
        }

        public static Api.Models.Membership MapProductType(int membershipId)
        {
            switch (membershipId)
            {
                case 4:
                    return Api.Models.Membership.Premium;
                case 3:
                    return Api.Models.Membership.Video;
                case 2:
                    return Api.Models.Membership.Book;
                default:
                    return Api.Models.Membership.None;
            }
        }
    }
}
