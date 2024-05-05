using System.ComponentModel.DataAnnotations;

namespace quanlinhanvien_masterdetails.Models
{
    public class PhongBanNhanVien
    {
        [Key]
        public int PhongBanNhanVien_Id { get; set; }
        [DataType(DataType.MultilineText)]
        public string MoTaCongViec { get; set; }
        [DataType(DataType.Date)]
        public DateTime NgayLamViec { get; set; } = DateTime.Today;
        public int? NhanVienId { get; set; }
        public NhanVien? NhanVien { get; set; }
        public int? PhongBanId { get; set; }
        public PhongBan? PhongBan { get; set; }
    }
}
