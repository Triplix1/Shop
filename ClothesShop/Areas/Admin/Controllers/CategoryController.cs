using ClothesShop.DataAccess.Repository.IRepository;
using ClothesShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothesShop.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //[Route("/admin/categories")]
        public IActionResult Index()
        {
            return View(_unitOfWork.Categories.GetAll().AsQueryable().Include(x => x.Sizes).ToList());
        }

        //[Route("/admin/categories/create")]
        public IActionResult Create()
        {;
            return View(new Category());
        }

        //[Route("/admin/categories")]
        [HttpPost]
        public IActionResult Create([FromForm] Category category)
        {
            CheckValidation(category);

            if (ModelState.IsValid)
            {
                _unitOfWork.Categories.Add(category);

                _unitOfWork.Save();

                TempData["success"] = "Created succsefully!";

                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        //[Route("/admin/categories/update/{id}")]
        public IActionResult Update(int id)
        {
            var category = _unitOfWork.Categories.GetAll(x => x.Id == id).AsQueryable().Include(x => x.Sizes).FirstOrDefault();

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        //[Route("/admin/categories")]
        [HttpPost]
        public IActionResult Update([FromForm] Category origin, [FromForm] Category category)
        {

            CheckValidation(category);

            if (ModelState.IsValid)
            {
                _unitOfWork.Categories.Update(category, origin);

                _unitOfWork.Save();

                TempData["success"] = "Updated succsefully!";

                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        //[Route("/admin/categories/delete-confirm/{Id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _unitOfWork.Categories.GetFirstOrDefault(u => u.Id == id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        //[Route("/admin/categories/{Id}")]
        [HttpPost]
        public IActionResult Delete([FromRoute] Category? category)
        {
            if (category == null)
            {
                TempData["Error"] = "This category is undefined";
            }
            else
            {
                _unitOfWork.Categories.Remove(category);

                _unitOfWork.Save();

                TempData["success"] = "Deleted succsefully!";
            }

            return RedirectToAction(nameof(Index));
        }

        private void CheckValidation(Category category)
        {
            if (ModelState.IsValid)
            {
                var categoryWithSameName = _unitOfWork.Categories.GetFirstOrDefault(x => x.CategoryName == category.CategoryName);
                if (categoryWithSameName != null)
                {
                    ModelState.AddModelError(nameof(category.CategoryName), "This category name has already been used");
                }
            }
        }

        #region API Methods
        [Route("/api/sizes-list/{id?}")]
        public IActionResult GetSizesListByCategoryId(int? id)
        {
            if(id == null || id == 0)
            {
                return Json(new { success = false });
            }   

            var category = _unitOfWork.Categories.GetFirstOrDefault(x => x.Id == id);

            if (category == null)
            {
                return Json(new { success = false });
            }

            return Json(new { success = true, sizes = category.Sizes });
        }
        #endregion

    }
}

