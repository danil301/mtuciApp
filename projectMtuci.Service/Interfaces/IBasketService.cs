using projectMtuci.Domain.Entity;
using projectMtuci.Domain.Response;
using projectMtuci.Domain.ViewModels.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectMtuci.Service.Interfaces
{
    public interface IBasketService
    {
        Task<IBaseResponse<BasketViewModel>> CreateBasketItem(BasketViewModel basketViewModel);

        IBaseResponse<bool> DeleteBasketItem(int id);

        Task<IBaseResponse<Basket>> GetBasketItem(int id);

        Task<IBaseResponse<IEnumerable<Basket>>> GetBasketItems();
    }
}
