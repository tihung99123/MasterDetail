using Microsoft.EntityFrameworkCore;
using quanlinhanvien_masterdetails.Models;

namespace quanlinhanvien_masterdetails.Data
{
    public class quanlinhanvien_masterdetailsContext : DbContext
    {
        public quanlinhanvien_masterdetailsContext(DbContextOptions<quanlinhanvien_masterdetailsContext> options)
            : base(options)
        {
        }

        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<PhongBan> PhongBans { get; set; }
        public DbSet<PhongBanNhanVien> PhongBanNhanViens { get; set; }
    }
}
