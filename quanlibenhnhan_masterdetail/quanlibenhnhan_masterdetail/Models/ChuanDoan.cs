using System.ComponentModel.DataAnnotations;

namespace quanlibenhnhan_masterdetail.Models
{
    public class ChuanDoan
    {
        [Key]
        public int Id { get; set; }
        public string TenChuanDoan { get; set; }
        public string? MoTa { get; set; }
        public IList<ChuanDoanBenhNhan> chuanDoanBenhNhans { get; set; } = new List<ChuanDoanBenhNhan>();

    }
}
