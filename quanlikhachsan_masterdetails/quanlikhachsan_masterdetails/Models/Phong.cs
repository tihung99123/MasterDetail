using System.ComponentModel.DataAnnotations;

namespace quanlikhachsan_masterdetails.Models
{
    public class Phong
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string SoPhong { get; set; }
        [DataType(DataType.Currency)]
        public decimal GiaPhong { get; set; }

        public IList<KhachDatPhong> khachDatPhongs { get; set; } = new List<KhachDatPhong>();
    }
}
