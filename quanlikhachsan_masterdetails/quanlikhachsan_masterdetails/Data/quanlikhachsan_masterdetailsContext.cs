using Microsoft.EntityFrameworkCore;
using quanlikhachsan_masterdetails.Models;

namespace quanlikhachsan_masterdetails.Data
{
    public class quanlikhachsan_masterdetailsContext : DbContext
    {
        public quanlikhachsan_masterdetailsContext(DbContextOptions<quanlikhachsan_masterdetailsContext> options)
            : base(options)
        {
        }

        public DbSet<Phong> Phongs { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<KhachDatPhong> KhachDatPhongs { get; set; }
    }
}
