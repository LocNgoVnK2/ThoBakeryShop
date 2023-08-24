using Infrastructure.Entities;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public interface IPromotionService
    {
        Task<IQueryable<Promotion>> GetPromotions();
        Task<Promotion> GetPromotion(int id);
        Task InsertPromotion(Promotion promotion);
        Task UpdatePromotion(Promotion promotion);
        Task DeletePromotion(Promotion promotion);
    }

    public class PromotionService : IPromotionService
    {
        private IPromotionRepository promotionRepository;

        public PromotionService(IPromotionRepository promotionRepository)
        {
            this.promotionRepository = promotionRepository;
        }

        public async Task<IQueryable<Promotion>> GetPromotions()
        {
            return await Task.FromResult(promotionRepository.GetAll());
        }

        public async Task<Promotion> GetPromotion(int id)
        {
            return await promotionRepository.GetByIdAsync(id);
        }

        public async Task InsertPromotion(Promotion promotion)
        {
            await promotionRepository.InsertAsync(promotion);

        }

        public async Task UpdatePromotion(Promotion promotion)
        {
            await promotionRepository.UpdateAsync(promotion);

        }

        public async Task DeletePromotion(Promotion promotion)
        {
            await promotionRepository.DeleteAsync(promotion);

        }
    }
 
}
