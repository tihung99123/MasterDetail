using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quanlikhachsan_masterdetails.Models
{
    public class KhachHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string TenKhachHang { get; set; }
        public string? DiaChi { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string? SoDienThoai { get; set; }
        public IList<KhachDatPhong> khachDatPhongs { get; set; } = new List<KhachDatPhong>();
    }
}
