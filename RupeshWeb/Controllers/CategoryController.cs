using Microsoft.AspNetCore.Mvc;
using RupeshWeb.Data;
using RupeshWeb.Models;

namespace RupeshWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _dbContext.Categories.ToList();
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
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();
                TempData["success"] = "Category created successfuly";
                return RedirectToAction("Index");
            }
            return View();

        }
        
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDB = _dbContext.Categories.Find(id);
            //Category? categoryFromDB1 = _dbContext.Categories.FirstOrDefault(u => u.Id == id);
            //Category? categoryFromDB2 = _dbContext.Categories.Where(u => u.Id == id).FirstOrDefault();
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
                _dbContext.Categories.Update(category);
                _dbContext.SaveChanges();
                TempData["success"] = "Category updated successfuly";
                return RedirectToAction("Index");
            }
            return View();

        }
        
        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDB = _dbContext.Categories.Find(id);
            //Category? categoryFromDB1 = _dbContext.Categories.FirstOrDefault(u => u.Id == id);
            //Category? categoryFromDB2 = _dbContext.Categories.Where(u => u.Id == id).FirstOrDefault();
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
            //Category? categoryFromDB = _dbContext.Categories.Find(id);
            //if (categoryFromDB == null)
            //{
            //    return NotFound();
            //}
            if (category == null)
            {
                return NotFound();
            }
            //_dbContext.Categories.Remove(categoryFromDB);
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            TempData["success"] = "Category deleted successfuly";
            return RedirectToAction("Index");
        }
    }
}
