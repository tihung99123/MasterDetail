using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quanlinhanvien_masterdetails.Data;
using quanlinhanvien_masterdetails.Models;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace quanlinhanvien_masterdetails.Controllers
{
    public class NhanViensController : Controller
    {
        private readonly quanlinhanvien_masterdetailsContext _context;

        public NhanViensController(quanlinhanvien_masterdetailsContext context, IWebHostEnvironment enc)
        {
            _context = context;
        }

        // GET: NhanViens
        public async Task<IActionResult> Index()
        {
            return View(await _context.NhanViens.ToListAsync());
        }

        // GET: NhanViens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens.Include(i => i.phongBanNhanViens).ThenInclude(r => r.PhongBan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        // GET: NhanViens/Create
        public IActionResult Create()
        {
            return View((new NhanVien()));
        }

        // POST: NhanViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NhanVien nhanVien, string action = "")
        {

            if (action == "Add")
            {
                nhanVien.phongBanNhanViens.Add(new());
                return View(nhanVien);
            }
            else if (action.Contains("delete"))// delete-3-sdsd-5   ["delete", "3"]
            {
                int idx = int.Parse(action.Split('-')[1]);

                nhanVien.phongBanNhanViens.RemoveAt(idx);
                ModelState.Clear();
                return View(nhanVien);
            }

            if (ModelState.IsValid)
            {
                var rows = await _context.Database.ExecuteSqlRawAsync("INSERT INTO NhanViens (TenNhanVien, GioiTinh, NgaySinh, Luong) VALUES (@p0, @p1, @p2, @p3)", nhanVien.TenNhanVien, nhanVien.GioiTinh, nhanVien.NgaySinh, nhanVien.Luong);

                if (rows > 0)
                {
                    nhanVien.Id = _context.NhanViens.Max(x => x.Id);

                    foreach (var item in nhanVien.phongBanNhanViens)
                    {
                        await _context.Database.ExecuteSqlRawAsync("INSERT INTO PhongBanNhanViens (PhongBanId, MoTaCongViec, NgayLamViec, NhanVienId) VALUES (@p0, @p1, @p2, @p3)", item.PhongBanId, item.MoTaCongViec, item.NgayLamViec, nhanVien.Id);
                    }

                }
                return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Index));
            }
            return View(nhanVien);
        }

        // GET: NhanViens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens.Include(i => i.phongBanNhanViens).ThenInclude(r => r.PhongBan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nhanVien == null)
            {
                return NotFound();
            }
            return View(nhanVien);
        }

        // POST: NhanViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NhanVien nhanVien, string action = "")
        {
            if (id != nhanVien.Id)
            {
                return NotFound();
            }
            if (action == "Add")
            {
                nhanVien.phongBanNhanViens.Add(new());
                return View(nhanVien);
            }
            else if (action.Contains("delete"))// delete-3-sdsd-5   ["delete", "3"]
            {
                int idx = int.Parse(action.Split('-')[1]);

                nhanVien.phongBanNhanViens.RemoveAt(idx);
                ModelState.Clear();
                return View(nhanVien);
            }


            if (ModelState.IsValid)
            {
                try
                {
                    var rows = await _context.Database.ExecuteSqlRawAsync("UPDATE NhanViens SET TenNhanVien = @p1, GioiTinh = @p2, NgaySinh = @p3, Luong = @p4 WHERE Id = @p0", nhanVien.Id, nhanVien.TenNhanVien, nhanVien.GioiTinh,nhanVien.NgaySinh, nhanVien.Luong);

                    await _context.Database.ExecuteSqlAsync($"DELETE FROM PhongBanNhanViens WHERE NhanVienId = {nhanVien.Id}");

                    foreach (var item in nhanVien.phongBanNhanViens)
                    {
                        await _context.Database.ExecuteSqlRawAsync("INSERT INTO PhongBanNhanViens (PhongBanId, NgayLamViec, MoTaCongViec, NhanVienId) VALUES (@p0, @p1, @p2, @p3)", item.PhongBanId, item.NgayLamViec, item.MoTaCongViec, nhanVien.Id);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhanVienExists(nhanVien.Id))
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
            return View(nhanVien);
        }

        // GET: NhanViens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens.Include(i => i.phongBanNhanViens).ThenInclude(r => r.PhongBan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        // POST: NhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nhanVien = await _context.NhanViens.FindAsync(id);
            if (nhanVien != null)
            {
                await _context.Database.ExecuteSqlAsync($"DELETE FROM PhongBanNhanViens WHERE NhanVienId = {id}");
                await _context.Database.ExecuteSqlAsync($"DELETE FROM NhanViens WHERE Id = {id}");
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhanVienExists(int id)
        {
            return _context.NhanViens.Any(e => e.Id == id);
        }
    }
}
