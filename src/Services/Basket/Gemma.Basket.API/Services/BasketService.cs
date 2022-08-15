using AutoMapper;
using Gemma.Basket.API.DataAccess;
using Gemma.Basket.API.Entities;
using Gemma.Basket.API.Models.Requests;
using Gemma.Basket.API.Models.Responses;
using Gemma.Basket.API.Services.Interfaces;
using Gemma.Shared.Common;
using Gemma.Shared.Constants;
using Gemma.Shared.Extensions;
using Newtonsoft.Json;
using ILogger = Serilog.ILogger;

namespace Gemma.Basket.API.Services
{
    public class BasketService: IBasketService
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IBasketRepository _basketRepository;

        public BasketService(IMapper mapper, ILogger logger, IBasketRepository basketRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _basketRepository = basketRepository;
        }

        public async Task<Result<ShoppingCartResponse>> GetBasket(string username)
        {
            _logger.Here().MethodEnterd();

            var response = await _basketRepository.GetBasket(username); 
            var result = _mapper.Map<ShoppingCartResponse>(response);   

            if(result == null)
            {
                _logger.Here().Information("@{errorCode}: No basket was found for the user @{userName}", ErrorCodes.NotFound, username);
                return Result<ShoppingCartResponse>.Success(new ShoppingCartResponse());
            }

            _logger.Here().MethodExited();

            return Result<ShoppingCartResponse>.Success(result);
        }
        public async Task<Result<ShoppingCartResponse>> UpdateBasket(ShoppingCartRequest request)
        {
            _logger.Here().MethodEnterd();
            var basketToUpdate = _mapper.Map<ShoppingCart>(request);
            var isBasketExistWithUsername = await _basketRepository.GetBasket(request.UserName);
            if (isBasketExistWithUsername == null)
            {
                basketToUpdate.Id = Guid.NewGuid();
            }
            else
            {
                basketToUpdate.Id = isBasketExistWithUsername.Id;
            }
            var response = await _basketRepository.UpdateBasket(basketToUpdate);
            var result = _mapper.Map<ShoppingCartResponse>(response);

            _logger.Here().MethodExited();

            return Result<ShoppingCartResponse>.Success(result);
        }
        public async Task DeleteBasket(string username)
        {
            _logger.Here().MethodEnterd();
            await _basketRepository.DeleteBasket(username);
            _logger.Here().MethodExited();
        }
    }
}
