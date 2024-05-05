using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quanlibenhnhan_masterdetail.Models
{
    public class PhongBenhNhan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhongBenhNhan_ID { get; set; }
        [Required]
        public int? PhongId { get; set; }
        [DataType(DataType.Date)]
        public DateTime Check_In { get; set; } = DateTime.Today;
        public DateTime Check_Out { get; set; } = DateTime.Today.AddDays(1);
        public decimal TongTien => this.Phong is null ? 0 : this.Phong.TienPhong * (this.Check_Out - this.Check_In).Days;
        public int? BenhNhanId { get; set; }
        public Phong? Phong { get; set; }
        public BenhNhan? BenhNhan { get; set; }
    }
}
