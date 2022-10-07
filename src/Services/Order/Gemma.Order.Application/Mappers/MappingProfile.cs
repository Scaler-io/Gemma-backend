using AutoMapper;
using EventBus.Message.Events;
using EventBus.Message.Models;
using Gemma.Order.Application.Models.Dtos;
using Gemma.Order.Application.Models.Dtos.Order;
using Gemma.Order.Application.Models.Requests;
using Gemma.Order.Application.Models.Requests.Checkout;
using Gemma.Order.Application.Models.Requests.OrderDelete;
using Gemma.Order.Application.Models.Requests.OrderUpdate;
using Gemma.Order.Domain.Entities;

namespace Gemma.Order.Application.Mappers
{
    using Order = Order.Domain.Entities.Order;
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrdersDto>()
                .ForMember(d => d.Address, o => o.MapFrom(s => s.Address))
                .ForMember(d => d.PaymentDetails, o => o.MapFrom(s => s.PaymentDetails))
                .ForMember(d => d.MetaData, o => o.MapFrom(s => new MetadataDto {
                    CreatedAt = s.CreatedAt.ToString("dd/MM/yyyy hh:mm:ss"),
                    CreatedBy = s.CreatedBy,
                    LastModifiedBy = s.LastModifiedBy,
                    LastModifiedDate = s.LastModifiedAt.ToString()
                })).ReverseMap();

            CreateMap<BillingAddress, BillingAddressDto>().ReverseMap();
            CreateMap<PaymentDetails, PaymentDetailsDto>().ReverseMap();


            CreateMap<CheckoutOrderRequest, Order>()
                .ForMember(d => d.Address, o => o.MapFrom(s => s.Address))
                .ForMember(d => d.PaymentDetails, o => o.MapFrom(s => s.PaymentDetails))
                .ReverseMap();
            

            CreateMap<UpdateOrderRequest, Order>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.OrderId))
                .ForMember(d => d.Address, o => o.MapFrom(s => s.Address))
                .ForMember(d => d.PaymentDetails, o => o.MapFrom(s => s.PaymentDetails))
                .ReverseMap();

            CreateMap<DeleteOrderRequest, Order>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.OrderId))
                .ReverseMap();

            CreateMap<BillingAddressRequest, BillingAddress>().ReverseMap();
            CreateMap<PaymentDetailsRequest, PaymentDetails>().ReverseMap();

            CreateMap<CheckoutOrderRequest, BasketCheckoutEvent>()
                .ForMember(s => s.Address, o => o.MapFrom(d => d.Address))
                .ForMember(s => s.PaymentDetails, o => o.MapFrom(d => d.PaymentDetails))
                .ReverseMap();

            CreateMap<CheckoutOrderBillingAddress, BillingAddressRequest>().ReverseMap();
            CreateMap<CheckoutOrderPaymentDetails, PaymentDetailsRequest>().ReverseMap();
        }
    }
}
