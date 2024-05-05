using System.ComponentModel.DataAnnotations;

namespace quanlibenhnhan_masterdetail.Models
{
    public class Phong
    {
        [Key]
        public int Id { get; set; }
        public string SoPhong { get; set; }
        [DataType(DataType.Currency)]
        public decimal TienPhong { get; set; }
        public IList<PhongBenhNhan> phongBenhNhans { get; set; } = new List<PhongBenhNhan>();
    }
}
