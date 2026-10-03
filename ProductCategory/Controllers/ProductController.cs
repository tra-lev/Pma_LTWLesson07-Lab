using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductCategory.Models;

namespace ProductCategory.Controllers
{
    public class ProductController : Controller
    {
        private static readonly string[] AllowedImageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

        private static List<Category> categories = new List<Category>
        {
            new Category { Id = 1, Name = "Điện thoại" },
            new Category { Id = 2, Name = "Máy tính xách tay" },
            new Category { Id = 3, Name = "Máy tính bảng" },
            new Category { Id = 4, Name = "Phụ kiện" }
        };

        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "iPhone 15 Pro Max", Image = "", Price = 30000000, SalePrice = 27000000, Description = "Điện thoại cao cấp của Apple.", CategoryId = 1 },
            new Product { Id = 2, Name = "Dell XPS 13 Plus", Image = "", Price = 35000000, SalePrice = 31000000, Description = "Laptop mỏng nhẹ, hiệu năng cao.", CategoryId = 2 },
            new Product { Id = 3, Name = "Tai nghe AirPods Pro", Image = "", Price = 6000000, SalePrice = 5000000, Description = "Tai nghe không dây chống ồn.", CategoryId = 4 }
        };

        private readonly IWebHostEnvironment _env;

        public ProductController(IWebHostEnvironment env)
        {
            _env = env;
        }

        // GET: Product
        public IActionResult Index()
        {
            products.ForEach(LoadCategory);
            return View(products);
        }

        // GET: Product/Details/5
        public IActionResult Details(int? id)
        {
            var product = FindProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product, IFormFile? imageFile)
        {
            ModelState.Remove("Image");
            if (imageFile == null || imageFile.Length == 0)
            {
                ModelState.AddModelError("Image", "Vui lòng chọn hình ảnh.");
            }
            else
            {
                ValidateImageFile(imageFile);
            }
            ValidateCategory(product);

            if (ModelState.IsValid)
            {
                product.Image = SaveImage(imageFile!);
                product.Id = products.Count == 0 ? 1 : products.Max(p => p.Id) + 1;
                products.Add(product);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Product/Edit/5
        public IActionResult Edit(int? id)
        {
            var product = FindProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product, IFormFile? imageFile)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            var existing = products.FirstOrDefault(p => p.Id == id);
            if (existing == null)
            {
                return NotFound();
            }

            ModelState.Remove("Image");
            if (imageFile != null && imageFile.Length > 0)
            {
                ValidateImageFile(imageFile);
            }
            ValidateCategory(product);

            if (ModelState.IsValid)
            {
                existing.Name = product.Name;
                existing.Price = product.Price;
                existing.SalePrice = product.SalePrice;
                existing.Description = product.Description;
                existing.CategoryId = product.CategoryId;
                if (imageFile != null && imageFile.Length > 0)
                {
                    existing.Image = SaveImage(imageFile);
                }
                return RedirectToAction(nameof(Index));
            }
            product.Image = existing.Image;
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Product/Delete/5
        public IActionResult Delete(int? id)
        {
            var product = FindProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                products.Remove(product);
            }
            return RedirectToAction(nameof(Index));
        }

        private Product? FindProduct(int? id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                LoadCategory(product);
            }
            return product;
        }

        private void LoadCategory(Product product)
        {
            product.Category = categories.FirstOrDefault(c => c.Id == product.CategoryId);
        }

        private void ValidateCategory(Product product)
        {
            if (ModelState.ContainsKey("CategoryId") && ModelState["CategoryId"]!.Errors.Count > 0)
            {
                return;
            }
            if (!categories.Any(c => c.Id == product.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Danh mục đã chọn không tồn tại, vui lòng chọn lại.");
            }
        }

        private void ValidateImageFile(IFormFile imageFile)
        {
            var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
            if (!AllowedImageExtensions.Contains(extension))
            {
                ModelState.AddModelError("Image", "Chỉ cho phép file ảnh có đuôi: " + string.Join(", ", AllowedImageExtensions) + ".");
            }
        }

        private string SaveImage(IFormFile imageFile)
        {
            var folder = Path.Combine(_env.WebRootPath, "products");
            Directory.CreateDirectory(folder);

            var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(imageFile.FileName).ToLowerInvariant();
            using (var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }
            return fileName;
        }
    }
}
