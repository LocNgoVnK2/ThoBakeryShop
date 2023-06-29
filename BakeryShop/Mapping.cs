using AutoMapper;
using BakeryShop.Models;
using Infrastructure.Entities;

namespace BakeryShop
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            // account
            CreateMap<Accounts, AccountsViewModel>();
            CreateMap<AccountsViewModel, Accounts>();
            // account
            CreateMap<Category, CategoryViewModel>();
            CreateMap<CategoryViewModel, Category>();
        }
    }
}
