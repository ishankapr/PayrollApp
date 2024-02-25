using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImportDataService.Models
{
    [Table("EmployeeInfo")]
    public class EmployeeInfo
    {
        [Key]
        [Column("EmployeeInfoID")]
        public int EmployeeInfoID { get; set; }

        [Column("EmployeeID")]
        public int EmployeeID { get; set; }

        [Column("DepartmentID")]
        public int DepartmentID { get; set; }

        [Column("Date")]
        public DateTime Date { get; set; }

        [Column("Amount")]
        public decimal Amount { get; set; }

        [Column("Status")]
        public string Status { get; set; }
    }
}
