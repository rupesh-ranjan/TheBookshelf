using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rupesh.DataAccess.Data;
using Rupesh.DataAccess.Repository.IRepository;
using Rupesh.Models;
using Rupesh.Utility;

namespace RupeshWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot match the Name");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(category);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfuly";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDB = _unitOfWork.Category.Get(u => u.Id == id);
            //Category? categoryFromDB1 = _unitOfWork.Category.Categories.FirstOrDefault(u => u.Id == id);
            //Category? categoryFromDB2 = _unitOfWork.Category.Categories.Where(u => u.Id == id).FirstOrDefault();
            if (categoryFromDB == null)
            {
                return NotFound();
            }

            return View(categoryFromDB);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot match the Name");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfuly";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDB = _unitOfWork.Category.Get(u => u.Id == id);
            //Category? categoryFromDB1 = _unitOfWork.Category.Categories.FirstOrDefault(u => u.Id == id);
            //Category? categoryFromDB2 = _unitOfWork.Category.Categories.Where(u => u.Id == id).FirstOrDefault();
            if (categoryFromDB == null)
            {
                return NotFound();
            }

            return View(categoryFromDB);
        }
        [HttpPost]
        //[HttpPost, ActionName("Delete")]
        //public IActionResult Delete(int? id)
        public IActionResult Delete(Category category)
        {
            //Category? categoryFromDB = _unitOfWork.Category.Categories.Find(id);
            //if (categoryFromDB == null)
            //{
            //    return NotFound();
            //}
            if (category == null)
            {
                return NotFound();
            }
            //_unitOfWork.Category.Categories.Remove(categoryFromDB);
            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfuly";
            return RedirectToAction("Index");
        }
    }
}
