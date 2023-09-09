using projectMtuci.DAL.Interfaces;
using projectMtuci.DAL.Repositories;
using projectMtuci.Domain.Entity;
using projectMtuci.Domain.Enum;
using projectMtuci.Domain.Response;
using projectMtuci.Domain.ViewModels.Basket;
using projectMtuci.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectMtuci.Service.Implementations
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;

        public BasketService(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<IBaseResponse<BasketViewModel>> CreateBasketItem(BasketViewModel basketViewModel)
        {
            var baseResponse = new BaseResponse<BasketViewModel>();
            try
            {
                var basket = new Basket()
                {
                    UserName = basketViewModel.UserName,
                    SubjectId = basketViewModel.SubjectId,
                };
                await _basketRepository.Create(basket);
            }
            catch (Exception ex)
            {
                return new BaseResponse<BasketViewModel>()
                {
                    Description = $"[CreateBasketItem] : {ex.Message}"
                };
            }

            return baseResponse;
        }

        public IBaseResponse<bool> DeleteBasketItem(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var basketItem = _basketRepository.Get(id);
                if (basketItem == null)
                {
                    baseResponse.Description = "Такого объекта не найдено";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                _basketRepository.Delete(basketItem.Result);

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteBasketItem] : {ex.Message}"
                };
            }
        }

        public Task<IBaseResponse<Basket>> GetBasketItem(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IBaseResponse<IEnumerable<Basket>>> GetBasketItems()
        {
            var baseResponse = new BaseResponse<IEnumerable<Basket>>();
            try
            {
                var basketItems = await _basketRepository.Select();
                if (basketItems.Count() == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
                baseResponse.Data = basketItems;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Basket>>()
                {
                    Description = $"[GetBasketItems] : {ex.Message}"
                };
            }
        }
    }
}
