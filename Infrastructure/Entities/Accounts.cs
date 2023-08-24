using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    [Table("Accounts")]
    public class Accounts
    {
        [Key]
        [Column("AccountID")]
        public int? AccountID { get; set; }

        [Column("Username")]
        public string Username { get; set; }

        [Column("Password")]
        public string Password { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("ValidationCode")]
        public string? ValidationCode { get; set; }

        [Column("Role")]
        public string Role { get; set; }
        [Column("EmployeeID")]
        public string? EmployeeID { get; set; }

    }
}
