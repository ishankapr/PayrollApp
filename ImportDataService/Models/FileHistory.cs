using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ImportDataService.Models
{
    [Table("FileHistory")]
    public class FileHistory
    {
        [Key]
        [Column("FileHistoryID")]
        public int FileHistoryID { get; set; }

        [Column("FileName")]
        public string FileName { get; set; }

        [Column("CreatedOn")]
        public DateTime CreatedOn { get; set; }
        
    }
}
