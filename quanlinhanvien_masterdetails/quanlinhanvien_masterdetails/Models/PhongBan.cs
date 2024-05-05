using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quanlinhanvien_masterdetails.Models
{
    public class PhongBan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TenPhongBan { get; set; }
        [DataType(DataType.MultilineText)]
        public string MoTa { get; set; }
        public IList<PhongBanNhanVien> phongBanNhanViens { get; set; } = new List<PhongBanNhanVien>();
    }
}
