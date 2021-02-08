using AutoMapper;
using ISUTest.Data.Entities;
using ISUTest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace ISUTest.Services.AutoMapper
{
    public class AppProfile: Profile
    {
        public AppProfile()
        {
            /** Reservation*/
            /*CreateMap<ApplicationUser, UserViewModel>()
             .ForMember(dest => dest.Mail, opt => opt.MapFrom(src => src.Email))
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NombreCompleto))
             .ForMember(dest => dest.Subscription, opt => opt.MapFrom(src => src.UserProfile.Subscription))
             .ForMember(dest => dest.SubscriptionId, opt => opt.MapFrom(src => src.UserProfile.SubscriptionId))
             .ReverseMap();*/           
            CreateMap<ReservationViewModel, Reservation>().ReverseMap();

            /**Contact **/
            CreateMap<ContactViewModel, AddContactViewModel>().ReverseMap();
           

            
        }
    }
}
