using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using quanlikhachsan_masterdetails.Data;
using quanlikhachsan_masterdetails.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace quanlikhachsan_masterdetails.Controllers
{
    public class KhachHangsController : Controller
    {
        private readonly quanlikhachsan_masterdetailsContext _context;

        public KhachHangsController(quanlikhachsan_masterdetailsContext context)
        {
            _context = context;
        }

        // GET: KhachHangs
        public async Task<IActionResult> Index()
        {
            var data = await _context.KhachHangs.Include(i => i.khachDatPhongs).ThenInclude(p => p.Phong).ToListAsync();


            ViewBag.Count = data.Count;
            ViewBag.TongCong = data.Sum(i => i.khachDatPhongs.Sum(l => l.TongTien));
            ViewBag.LoiNhuanTrungBinh = data.Count > 0 ? data.Average(i => i.khachDatPhongs.Sum(l => l.TongTien)) : 0;

            return View(data);
        }

        // GET: KhachHangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHangs.Include(i => i.khachDatPhongs).ThenInclude(r => r.Phong)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }

        // GET: KhachHangs/Create
        public IActionResult Create()
        {
            return View((new KhachHang()));
        }

        // POST: KhachHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KhachHang khachHang, string action = "")
        {
            if (action == "action_Add")
            {
                khachHang.khachDatPhongs.Add(new());
                return View(khachHang);
            }
            else if (action.Contains("action_delete"))
            {
                int idx = int.Parse(action.Split('-')[1]);

                khachHang.khachDatPhongs.RemoveAt(idx);
                ModelState.Clear();
                return View(khachHang);
            }

            if (ModelState.IsValid)
            {
                var rows = await _context.Database.ExecuteSqlRawAsync("INSERT INTO KhachHangs (TenKhachHang, DiaChi, SoDienThoai) VALUES (@p0, @p1, @p2)", khachHang.TenKhachHang, khachHang.DiaChi, khachHang.SoDienThoai);

                if (rows > 0)
                {
                    khachHang.Id = _context.KhachHangs.Max(x => x.Id);

                    foreach (var item in khachHang.khachDatPhongs)
                    {
                        await _context.Database.ExecuteSqlRawAsync("INSERT INTO KhachDatPhongs (PhongId, Check_In, Check_Out, KhachHangId) VALUES (@p0, @p1, @p2, @p3)", item.PhongId, item.Check_In, item.Check_Out, khachHang.Id);
                    }

                }
                return RedirectToAction(nameof(Index));
            }
            return View(khachHang);
        }

        // GET: KhachHangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHangs.Include(i => i.khachDatPhongs).ThenInclude(r => r.Phong)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (khachHang == null)
            {
                return NotFound();
            }
            return View(khachHang);
        }

        // POST: KhachHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,KhachHang khachHang, string action = "")
        {
            if (id != khachHang.Id)
            {
                return NotFound();
            }

            if (action == "action_Add")
            {
                khachHang.khachDatPhongs.Add(new());
                return View(khachHang);
            }
            else if (action.Contains("action_delete"))
            {
                int idx = int.Parse(action.Split('-')[1]);

                khachHang.khachDatPhongs.RemoveAt(idx);
                ModelState.Clear();
                return View(khachHang);
            }


            if (ModelState.IsValid)
            {
                try
                {
                    var rows = await _context.Database.ExecuteSqlRawAsync("UPDATE KhachHangs SET TenKhachHang = @p1, DiaChi = @p2, SoDienThoai = @p3 WHERE Id = @p0", khachHang.Id, khachHang.TenKhachHang, khachHang.DiaChi, khachHang.SoDienThoai);

                    await _context.Database.ExecuteSqlAsync($"DELETE FROM KhachDatPhongs WHERE KhachHangId = {khachHang.Id}");

                    foreach (var item in khachHang.khachDatPhongs)
                    {
                        await _context.Database.ExecuteSqlRawAsync("INSERT INTO KhachDatPhongs (PhongId, Check_In, Check_Out, KhachHangId) VALUES (@p0, @p1, @p2, @p3)", item.PhongId, item.Check_In, item.Check_Out, khachHang.Id);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhachHangExists(khachHang.Id))
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
            return View(khachHang);
        }

        // GET: KhachHangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHangs.Include(i => i.khachDatPhongs).ThenInclude(r => r.Phong)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }

        // POST: KhachHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var khachHang = await _context.KhachHangs.FindAsync(id);

            if (khachHang != null)
            {

                await _context.Database.ExecuteSqlAsync($"DELETE FROM KhachDatPhongs WHERE KhachHangId = {id}");
                await _context.Database.ExecuteSqlAsync($"DELETE FROM KhachHang WHERE Id = {id}");
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KhachHangExists(int id)
        {
            return _context.KhachHangs.Any(e => e.Id == id);
        }
    }
}
