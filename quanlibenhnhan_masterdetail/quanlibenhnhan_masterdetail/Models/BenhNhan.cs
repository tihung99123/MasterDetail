using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace quanlibenhnhan_masterdetail.Models
{
    public class BenhNhan
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string TenBenhNhan { get; set; }
        [DataType(DataType.Date)]
        public DateTime NgaySinh { get; set; } = DateTime.Today;
        [DisplayName("Nam")]
        public bool GioiTinh { get; set; }
        public IList<ChuanDoanBenhNhan> chuanDoanBenhNhans { get; set; } = new List<ChuanDoanBenhNhan>();
        public IList<PhongBenhNhan> phongBenhNhans { get; set; } = new List<PhongBenhNhan>();

    }
}
