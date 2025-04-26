using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModule;
using ServiceAbstraction;
using Shared.DTOS.BasketDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BasketServices(IBasketRepository _basketRepository , IMapper _mapper) : IBasketServices
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var customerBasket = _mapper.Map<BasketDto, CustomerBasket>(basket);
            var result = await _basketRepository.CreateOrUpdateBasketAsync(customerBasket);
            if(result is not null)
                return await GetBasketAsync(result.Id);
            else
                throw new Exception("Error while creating or updating the basket");
        }

        public Task<bool> DeleteBasketAsync(string Key)
        {
            return _basketRepository.DeleteBasketAsync(Key);
        }

        public async Task<BasketDto> GetBasketAsync(string Key)
        {
            var basket = await _basketRepository.GetBasketAsync(Key);
            if(basket is not null)
                return _mapper.Map<CustomerBasket,BasketDto>(basket);
            else
                throw new NotFoundBasketException(Key);
        }
    }
}
