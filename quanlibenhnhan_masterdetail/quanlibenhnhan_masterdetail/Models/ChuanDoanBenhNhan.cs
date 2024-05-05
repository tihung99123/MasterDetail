using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quanlibenhnhan_masterdetail.Models
{
    public class ChuanDoanBenhNhan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChuanDoanBenhNhan_Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime NgayChuanDoan { get; set; }
        public string? MoTaGiaoThuoc { get; set; }
        public int? ChuanDoanId { get; set; }
        public ChuanDoan? ChuanDoan { get; set; }
        public int? BenhNhanId { get; set; }
        public BenhNhan? BenhNhan { get; set; }

    }
}
