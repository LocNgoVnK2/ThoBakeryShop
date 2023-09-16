using Infrastructure.Entities;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public interface IReviewService
    {
        Task<IQueryable<Review>> GetReviews();
        Task<Review> GetReview(int reviewID);
        Task InsertReview(Review review);
        Task UpdateReview(Review review);
        Task DeleteReview(Review review);
    }

    public class ReviewService : IReviewService
    {
        private IReviewRepository reviewRepository;

        public ReviewService(IReviewRepository ReviewRepository)
        {
            this.reviewRepository = ReviewRepository;
        }

        public async Task<IQueryable<Review>> GetReviews()
        {
            return await Task.FromResult(reviewRepository.GetAll());
        }

        public async Task<Review> GetReview(int reviewID)
        {
            return await reviewRepository.GetByIdAsync(reviewID);
        }

        public async Task InsertReview(Review review)
        {
            await reviewRepository.InsertAsync(review);

        }

        public async Task UpdateReview(Review review)
        {
            await reviewRepository.UpdateAsync(review);

        }

        public async Task DeleteReview(Review review)
        {
            await reviewRepository.DeleteAsync(review);

        }
    }
}
