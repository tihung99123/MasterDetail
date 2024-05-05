using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quanlibenhnhan_masterdetail.Data;
using quanlibenhnhan_masterdetail.Models;

namespace quanlibenhnhan_masterdetail.Controllers
{
    public class BenhNhansController : Controller
    {
        private readonly quanlibenhnhan_masterdetailContext _context;

        public BenhNhansController(quanlibenhnhan_masterdetailContext context)
        {
            _context = context;
        }

        // GET: BenhNhans
        public async Task<IActionResult> Index()
        {
            return View(await _context.BenhNhans.ToListAsync());
        }

        // GET: BenhNhans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var benhNhan = await _context.BenhNhans
                .Include(i => i.chuanDoanBenhNhans).ThenInclude(r => r.ChuanDoan)
                .Include(i => i.phongBenhNhans).ThenInclude(r => r.Phong)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (benhNhan == null)
            {
                return NotFound();
            }

            return View(benhNhan);
        }

        // GET: BenhNhans/Create
        public IActionResult Create()
        {
            return View((new BenhNhan()));
        }

        // POST: BenhNhans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BenhNhan benhNhan, string action = "")
        {
            if (action == "ChuanDoan_Add")
            {
                benhNhan.chuanDoanBenhNhans.Add(new());
                return View(benhNhan);
            }
            else if (action.Contains("ChuanDoan_delete"))
            {
                int idx = int.Parse(action.Split('-')[1]);

                benhNhan.chuanDoanBenhNhans.RemoveAt(idx);
                ModelState.Clear();
                return View(benhNhan);
            }
            if (action == "Phong_Add")
            {
                benhNhan.phongBenhNhans.Add(new());
                return View(benhNhan);
            }
            else if (action.Contains("Phong_delete"))
            {
                int idx = int.Parse(action.Split('-')[1]);

                benhNhan.phongBenhNhans.RemoveAt(idx);
                ModelState.Clear();
                return View(benhNhan);
            }


            if (ModelState.IsValid)
            {
                var rows = await _context.Database.ExecuteSqlRawAsync("INSERT INTO BenhNhans (TenBenhNhan, NgaySinh, GioiTinh) VALUES (@p0, @p1, @p2)", benhNhan.TenBenhNhan, benhNhan.NgaySinh, benhNhan.GioiTinh);

                if (rows > 0)
                {
                    benhNhan.Id = _context.BenhNhans.Max(x => x.Id);

                    foreach (var item in benhNhan.chuanDoanBenhNhans)
                    {
                        await _context.Database.ExecuteSqlRawAsync("INSERT INTO ChuanDoanBenhNhans (NgayChuanDoan, MoTaGiaoThuoc, ChuanDoanId, BenhNhanId) VALUES (@p0, @p1, @p2, @p3)", item.NgayChuanDoan, item.MoTaGiaoThuoc, item.ChuanDoanId, benhNhan.Id);
                    }

                    foreach (var item in benhNhan.phongBenhNhans)
                    {
                        await _context.Database.ExecuteSqlRawAsync("INSERT INTO PhongBenhNhans (PhongId, Check_In, Check_Out, BenhNhanId) VALUES (@p0, @p1, @p2, @p3)", item.PhongId, item.Check_In, item.Check_Out, benhNhan.Id);
                    }

                }
                return RedirectToAction(nameof(Index));
            }
            return View(benhNhan);
        }

        // GET: BenhNhans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var benhNhan = await _context.BenhNhans
                .Include(i => i.chuanDoanBenhNhans).ThenInclude(r => r.ChuanDoan)
                .Include(i => i.phongBenhNhans).ThenInclude(r => r.Phong)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (benhNhan == null)
            {
                return NotFound();
            }
            return View(benhNhan);
        }

        // POST: BenhNhans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BenhNhan benhNhan, string action = "")
        {
            if (id != benhNhan.Id)
            {
                return NotFound();
            }

            if (action == "ChuanDoan_Add")
            {
                benhNhan.chuanDoanBenhNhans.Add(new());
                return View(benhNhan);
            }
            else if (action.Contains("ChuanDoan_delete"))
            {
                int idx = int.Parse(action.Split('-')[1]);

                benhNhan.chuanDoanBenhNhans.RemoveAt(idx);
                ModelState.Clear();
                return View(benhNhan);
            }
            if (action == "Phong_Add")
            {
                benhNhan.phongBenhNhans.Add(new());
                return View(benhNhan);
            }
            else if (action.Contains("Phong_delete"))
            {
                int idx = int.Parse(action.Split('-')[1]);

                benhNhan.phongBenhNhans.RemoveAt(idx);
                ModelState.Clear();
                return View(benhNhan);
            }


            if (ModelState.IsValid)
            {
                try
                {
                    var rows = await _context.Database.ExecuteSqlRawAsync("UPDATE BenhNhans SET TenBenhNhan = @p1, NgaySinh = @p2, GioiTinh = @p3 WHERE Id = @p0", benhNhan.Id, benhNhan.TenBenhNhan, benhNhan.NgaySinh, benhNhan.GioiTinh);

                    await _context.Database.ExecuteSqlAsync($"DELETE FROM ChuanDoanBenhNhans WHERE BenhNhanId = {benhNhan.Id}");
                    await _context.Database.ExecuteSqlAsync($"DELETE FROM PhongBenhNhans WHERE BenhNhanId = {benhNhan.Id}");

                    foreach (var item in benhNhan.chuanDoanBenhNhans)
                    {
                        await _context.Database.ExecuteSqlRawAsync("INSERT INTO ChuanDoanBenhNhans (NgayChuanDoan, MoTaGiaoThuoc, ChuanDoanId, BenhNhanId) VALUES (@p0, @p1, @p2, @p3)", item.NgayChuanDoan, item.MoTaGiaoThuoc, item.ChuanDoanId, benhNhan.Id);
                    }

                    foreach (var item in benhNhan.phongBenhNhans)
                    {
                        await _context.Database.ExecuteSqlRawAsync("INSERT INTO PhongBenhNhans (PhongId, Check_In, Check_Out, BenhNhanId) VALUES (@p0, @p1, @p2, @p3)", item.PhongId, item.Check_In, item.Check_Out, benhNhan.Id);
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BenhNhanExists(benhNhan.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(benhNhan);
        }

        // GET: BenhNhans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var benhNhan = await _context.BenhNhans
                .Include(i => i.chuanDoanBenhNhans).ThenInclude(r => r.ChuanDoan)
                .Include(i => i.phongBenhNhans).ThenInclude(r => r.Phong)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (benhNhan == null)
            {
                return NotFound();
            }

            return View(benhNhan);
        }

        // POST: BenhNhans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var benhNhan = await _context.BenhNhans.FindAsync(id);
            if (benhNhan != null)
            {
                await _context.Database.ExecuteSqlAsync($"DELETE FROM ChuanDoanBenhNhans WHERE BenhNhanId = {id}");
                await _context.Database.ExecuteSqlAsync($"DELETE FROM PhongBenhNhans WHERE BenhNhanId = {id}");
                await _context.Database.ExecuteSqlAsync($"DELETE FROM BenhNhans WHERE Id = {id}");
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BenhNhanExists(int id)
        {
            return _context.BenhNhans.Any(e => e.Id == id);
        }
    }
}
