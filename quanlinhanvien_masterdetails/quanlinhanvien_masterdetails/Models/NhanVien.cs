using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quanlinhanvien_masterdetails.Models
{
    public class NhanVien
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string TenNhanVien { get; set; }
        [Required]
        [DisplayName("Nữ")]
        public bool GioiTinh { get; set; }
        [DataType(DataType.Date)]
        public DateTime NgaySinh { get; set; } = DateTime.Today;
        [DataType(DataType.Currency)]
        public decimal Luong { get; set; }
        public IList<PhongBanNhanVien> phongBanNhanViens { get; set; } = new List<PhongBanNhanVien>();
    }
}
