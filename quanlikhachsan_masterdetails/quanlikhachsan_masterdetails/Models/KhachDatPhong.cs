using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quanlikhachsan_masterdetails.Models
{
    public class KhachDatPhong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int KhachDatPhong_Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Check_In { get; set; } = DateTime.Today;
        public DateTime Check_Out { get; set; } = DateTime.Today.AddDays(1);
        public decimal TongTien => this.Phong is null ? 0 : this.Phong.GiaPhong * (this.Check_Out - this.Check_In).Days;

        public int? PhongId { get; set; }
        public int? KhachHangId { get; set; }
        public Phong? Phong { get; set; }
        public KhachHang? KhachHang { get; set; }
    }
}
