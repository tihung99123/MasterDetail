using Microsoft.EntityFrameworkCore;
using quanlibenhnhan_masterdetail.Models;

namespace quanlibenhnhan_masterdetail.Data
{
    public class quanlibenhnhan_masterdetailContext : DbContext
    {
        public quanlibenhnhan_masterdetailContext(DbContextOptions<quanlibenhnhan_masterdetailContext> options)
            : base(options)
        {
        }

        public DbSet<BenhNhan> BenhNhans { get; set; }
        public DbSet<ChuanDoan> ChuanDoans { get; set; }
        public DbSet<ChuanDoanBenhNhan> ChuanDoanBenhNhans { get; set; }
        public DbSet<Phong> Phongs { get; set; }
        public DbSet<PhongBenhNhan> PhongBenhNhans { get; set; }
    }
}
