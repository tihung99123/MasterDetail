using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quanlibenhnhan_masterdetail.Data;
using quanlibenhnhan_masterdetail.Models;

namespace quanlibenhnhan_masterdetail.Controllers
{
    public class ChuanDoansController : Controller
    {
        private readonly quanlibenhnhan_masterdetailContext _context;

        public ChuanDoansController(quanlibenhnhan_masterdetailContext context)
        {
            _context = context;
        }

        // GET: ChuanDoans
        public async Task<IActionResult> Index()
        {
            return View(await _context.ChuanDoans.ToListAsync());
        }

        // GET: ChuanDoans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chuanDoan = await _context.ChuanDoans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chuanDoan == null)
            {
                return NotFound();
            }

            return View(chuanDoan);
        }

        // GET: ChuanDoans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ChuanDoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TenChuanDoan,MoTa")] ChuanDoan chuanDoan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chuanDoan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chuanDoan);
        }

        // GET: ChuanDoans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chuanDoan = await _context.ChuanDoans.FindAsync(id);
            if (chuanDoan == null)
            {
                return NotFound();
            }
            return View(chuanDoan);
        }

        // POST: ChuanDoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TenChuanDoan,MoTa")] ChuanDoan chuanDoan)
        {
            if (id != chuanDoan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chuanDoan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChuanDoanExists(chuanDoan.Id))
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
            return View(chuanDoan);
        }

        // GET: ChuanDoans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chuanDoan = await _context.ChuanDoans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chuanDoan == null)
            {
                return NotFound();
            }

            return View(chuanDoan);
        }

        // POST: ChuanDoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chuanDoan = await _context.ChuanDoans.FindAsync(id);
            if (chuanDoan != null)
            {
                _context.ChuanDoans.Remove(chuanDoan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChuanDoanExists(int id)
        {
            return _context.ChuanDoans.Any(e => e.Id == id);
        }
    }
}
