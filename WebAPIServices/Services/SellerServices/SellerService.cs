using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIServices.Models.DTO;
using WebAPIServices.Services.SuperHeroService;

namespace WebAPIServices.Services.SellerServices
{
    public class SellerService : ISellerService
    {
        private static List<SellerDto> sellers = new List<SellerDto>
    {
        new SellerDto { Id = 1, Name = "Spider Man", Email = "Peter@gmail.com", Password = "Parker" },
        new SellerDto { Id = 2, Name = "Iron Man", Email = "Tony@gmail.com", Password = "Stark" }
    };

        public async Task<SellerDto> AddSellerAsync(SellerDto seller)
        {
            sellers.Add(seller);
            return await Task.FromResult(seller);
        }

        public async Task<List<SellerDto>> DeleteSellerAsync(int id)
        {
            var seller = sellers.Find(s => s.Id == id);
            if (seller != null)
            {
                sellers.Remove(seller);
            }
            return await Task.FromResult(sellers);
        }

        public async Task<List<SellerDto>> GetAllSellersAsync()
        {
            return await Task.FromResult(sellers);
        }

        public async Task<SellerDto> GetSingleSellerAsync(int id)
        {
            return await Task.FromResult(sellers.FirstOrDefault(s => s.Id == id));
        }

        public async Task<SellerDto> UpdateSellerAsync(int id, SellerDto seller)
        {
            var existingSeller = sellers.FirstOrDefault(s => s.Id == id);
            if (existingSeller != null)
            {
                existingSeller.Name = seller.Name;
                existingSeller.Email = seller.Email;
                existingSeller.Password = seller.Password;
            }
            return await Task.FromResult(existingSeller);
        }

        public async Task<string> SerializeSellerAsync(SellerDto seller)
        {
            string json = JsonConvert.SerializeObject(seller);
            return await Task.FromResult(json);
        }

        public async Task<SellerDto> DeserializeSellerAsync(string json)
        {
            SellerDto seller = JsonConvert.DeserializeObject<SellerDto>(json);
            return await Task.FromResult(seller);
        }
    }
}
