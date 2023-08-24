using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    [Table("Employees")]
    public class Employee
    {
            [Key]
            [Column("EmployeeID")]
            public string EmployeeID { get; set; }

            [Column("FirstName")]
            public string FirstName { get; set; }

            [Column("LastName")]
            public string LastName { get; set; }
            [Column("Position")]
            public string Position { get; set; }
            [Column("PhoneNumber")]
            public string PhoneNumber { get; set; }
    }
}
