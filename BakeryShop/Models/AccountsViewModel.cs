using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BakeryShop.Models
{
    public class AccountsViewModel
    {

        public int AccountID { get; set; }

        public string Username { get; set; }

      
        public string Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string? ValidationCode { get; set; }
        public string Role { get; set; }

        public int? EmployeeID { get; set; }
    }
}
