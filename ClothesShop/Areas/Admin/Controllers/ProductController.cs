using ClothesShop.DataAccess.Repository.IRepository;
using ClothesShop.Models;
using ClothesShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClothesShop.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View(_unitOfWork.Products.GetAll());
        }

        public IActionResult Create()
        {
            return View(new ProductVM
            {
                CategoriesList = _unitOfWork.Categories.GetAll()
                .Select(c => new SelectListItem
                {
                    Text = c.CategoryName,
                    Value = c.Id.ToString()
                }).ToList()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductVM product)
        {
            if (ModelState.IsValid)
            {
                if (product.MainPhoto != null)
                {
                    string folder = "images/products/main/";

                    product.MainImageUrl = await UploadImage(folder, product.MainPhoto);
                }

                if (product.GalleryFiles != null)
                {
                    string folder = "images/products/gallery/";

                    product.Gallery = new List<GalleryModel>();

                    foreach (var file in product.GalleryFiles)
                    {
                        product.Gallery.Add(new GalleryModel
                        {
                            Name = file.Name,
                            URL = await UploadImage(folder, file)
                        });
                    }
                }

                Product resultedProduct = new Product
                {
                    Name = product.Name,
                    CategoryId = product.CategoryId,
                    Description = product.Description,
                    Price = product.Price,
                    MainPhotoUrl = product.MainImageUrl,
                    ImagesUrl = product.Gallery.Select(c => new ProductGallery
                    {
                        Name = c.Name,
                        URL = c.URL
                    }).ToList()
                };

                _unitOfWork.Products.Add(resultedProduct);

                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(product);
            
            //if (ModelState.IsValid)
            //{
            //    if (product.Sizes == null || product.Sizes.Any())
            //    {
            //        ModelState.AddModelError(nameof(product.Sizes), "Sizes list should contains elements");
            //    }
            //}

            //if (ModelState.IsValid)
            //{
            //    _unitOfWork.Products.Add(product);
            //    _unitOfWork.Save();
            //    return RedirectToAction(nameof(Index));
            //}

            return View();
        }

        public IActionResult Update([FromRoute] int id)
        {
            var product = _unitOfWork.Products.GetFirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                TempData["Error"] = "This product is undefined";
                RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        [HttpPost]
        public IActionResult Update([FromForm] Product product, Product? origin)
        {
            _unitOfWork.Products.Update(product, origin);

            _unitOfWork.Save();

            TempData["success"] = "Updated succsefully!";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _unitOfWork.Products.GetFirstOrDefault(u => u.Id == id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public IActionResult Delete([FromForm] Product? product)
        {
            if (product == null)
            {
                TempData["Error"] = "This product is undefined";
            }
            else
            {
                _unitOfWork.Products.Remove(product);

                _unitOfWork.Save();

                TempData["success"] = "Deleted succsefully!";
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {
            if (file != null)
            {
                folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

                string wwwrootPath = _hostEnvironment.WebRootPath;

                string serverFolder = Path.Combine(wwwrootPath, folderPath);

                await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

                return "/" + folderPath;
            }
            else
            {
                throw new ArgumentNullException(nameof(file));
            }
        }
    }
}
