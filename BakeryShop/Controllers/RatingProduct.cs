using BakeryShop.Models;
using Infrastructure.Entities;
using Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;

namespace BakeryShop.Controllers
{
    public class RatingProduct : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly ICustomerService _customerService;
        public RatingProduct(IReviewService reviewService,ICustomerService customerService) { 
            _customerService = customerService;
            _reviewService = reviewService;
        }
        public IActionResult Index(int orderId)
        {
            // for theo ma sp r load vào 
            return View();
        }
        
        public async Task<IActionResult> AddRating( RatingViewModel model) { 
            Customer customer = await _customerService.GetCustomerByPhoneNumber(model.PhoneNumber);
            Review review = new Review()
            {
                Comment = model.Comment,
                Rating = model.Rating,
                ProductID = model.ProductID,
                CustomerID = customer.CustomerId
                
            };
            await _reviewService.InsertReview(review);
            TempData["ValidationCode"] = customer.ValidationCode;
            TempData["PhoneNumber"] = customer.PhoneNumber;
            return RedirectToAction("ReviewOrder","Cart");
        }
        
    }
}
