using AutoMapper;
using Gemma.Basket.API.DataAccess.Interface;
using Gemma.Basket.API.Entities;
using Gemma.Basket.API.Models.Requests;
using Gemma.Basket.API.Models.Responses;
using Gemma.Basket.API.Services.Interface;
using Gemma.Shared.Common;
using Gemma.Shared.Extensions;
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

        public async Task<Result<BasketResponse>> GetBasket(string username)
        {
            _logger.Here().MethodEnterd();
            var basket = await _basketRepository.GetBasket(username);

            if(basket == null)
            {
                _logger.Here().Information("No basket was found for username {@userName}", username);
                var emptyBasket = _mapper.Map<BasketResponse>(new ShoppingCart(username));
                return Result<BasketResponse>.Success(emptyBasket);
            }

            var result = _mapper.Map<BasketResponse>(basket);
            _logger.Here().MethodExited();

            return Result<BasketResponse>.Success(result);
        }
        public async Task<Result<BasketResponse>> UpdateBasket(BasketRequest request)
        {
            _logger.Here().MethodEnterd();

            var shoppingCart = _mapper.Map<ShoppingCart>(request);
            var basket = await _basketRepository.UpdateBasket(shoppingCart);

            var result = _mapper.Map<BasketResponse>(basket);

            _logger.Here().Information("Basket updated. {@basket}", basket);
            _logger.Here().MethodExited();

            return Result<BasketResponse>.Success(result);
        }


        public async Task DeleteBasket(string username)
        {
            _logger.Here().MethodEnterd();
            await _basketRepository.DeleteBasket(username);
            _logger.Here().MethodEnterd();
        }
        
    }
}
