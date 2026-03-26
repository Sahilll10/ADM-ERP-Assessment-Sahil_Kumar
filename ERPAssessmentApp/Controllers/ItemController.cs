using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERPAssessmentApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAssessmentApp.Controllers
{
    public class ItemController : Controller
    {
        private readonly AppDbContext _context;

        public ItemController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var items = from m in _context.Items select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.ItemName.Contains(searchString));
            }

            return View(await items.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Item item)
        {
            ModelState.Clear();

            if (string.IsNullOrWhiteSpace(item.ItemName))
            {
                ModelState.AddModelError("ItemName", "Please enter an item name.");
            }

            if (item.Weight <= 0)
            {
                ModelState.AddModelError("Weight", "Weight must be greater than zero.");
            }

            if (ModelState.IsValid)
            {
                item.IsProcessed = false;
                item.ParentItemId = null;

                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(item);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var item = await _context.Items.FindAsync(id);
            if (item == null) return NotFound();

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Item item)
        {
            if (id != item.ItemId) return NotFound();

            ModelState.Clear();

            if (string.IsNullOrWhiteSpace(item.ItemName))
            {
                ModelState.AddModelError("ItemName", "Please enter an item name.");
            }

            if (item.Weight <= 0)
            {
                ModelState.AddModelError("Weight", "Weight must be greater than zero.");
            }

            if (ModelState.IsValid)
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Process(int? id)
        {
            if (id == null) return NotFound();

            var parentItem = await _context.Items.FindAsync(id);
            if (parentItem == null) return NotFound();
            if (parentItem.IsProcessed) return RedirectToAction(nameof(Index));

            return View(parentItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Process(int id, string[] ChildItemNames, decimal[] ChildWeights)
        {
            var parentItem = await _context.Items.FindAsync(id);
            if (parentItem == null) return NotFound();

            if (ChildItemNames == null || ChildItemNames.Length == 0)
            {
                ViewBag.Error = "Please add at least one child item.";
                return View(parentItem);
            }

            for (int i = 0; i < ChildItemNames.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(ChildItemNames[i]) && ChildWeights[i] > 0)
                {
                    var childItem = new Item
                    {
                        ItemName = ChildItemNames[i],
                        Weight = ChildWeights[i],
                        ParentItemId = parentItem.ItemId,
                        IsProcessed = false
                    };
                    _context.Items.Add(childItem);
                }
            }

            parentItem.IsProcessed = true;
            _context.Update(parentItem);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Tree()
        {
            var items = await _context.Items.ToListAsync();
            return View(items);
        }
    }
}