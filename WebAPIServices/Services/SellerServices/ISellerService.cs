using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPIServices.Models.DTO;

namespace WebAPIServices.Services.SuperHeroService
{
    public interface ISellerService
    {
        Task<List<SellerDto>> GetAllSellersAsync();
        Task<SellerDto> GetSingleSellerAsync(int id);
        Task<SellerDto> AddSellerAsync(SellerDto seller);
        Task<SellerDto> UpdateSellerAsync(int id, SellerDto seller);
        Task<List<SellerDto>> DeleteSellerAsync(int id);
    }
}
