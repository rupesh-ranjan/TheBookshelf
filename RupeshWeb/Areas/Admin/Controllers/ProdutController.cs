using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using Rupesh.DataAccess.Data;
using Rupesh.DataAccess.Repository.IRepository;
using Rupesh.Models;
using Rupesh.Models.ViewModels;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace RupeshWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
            return View(objProductList);
        }
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            //ViewBag.CategoryList = CategoryList;
            //ViewData["CategoryList"] = CategoryList;
            ProductVM productVM = new()
            {
                CategoryList = CategoryList,
                Product = new Product()
            };
            return View(productVM);
        }
        [HttpPost]
        public IActionResult Create(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product created successfuly";
                return RedirectToAction("Index");
            } else
            { 
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productVM);

            }
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDB = _unitOfWork.Product.Get(u => u.Id == id);
            //Product? ProductFromDB1 = _unitOfWork.Product.Categories.FirstOrDefault(u => u.Id == id);
            //Product? ProductFromDB2 = _unitOfWork.Product.Categories.Where(u => u.Id == id).FirstOrDefault();
            if (productFromDB == null)
            {
                return NotFound();
            }

            return View(productFromDB);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(product);
                _unitOfWork.Save();
                TempData["success"] = "Product updated successfuly";
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
            Product? productFromDB = _unitOfWork.Product.Get(u => u.Id == id);
            //Product? ProductFromDB1 = _unitOfWork.Product.Categories.FirstOrDefault(u => u.Id == id);
            //Product? ProductFromDB2 = _unitOfWork.Product.Categories.Where(u => u.Id == id).FirstOrDefault();
            if (productFromDB == null)
            {
                return NotFound();
            }

            return View(productFromDB);
        }
        [HttpPost]
        //[HttpPost, ActionName("Delete")]
        //public IActionResult Delete(int? id)
        public IActionResult Delete(Product product)
        {
            //Product? ProductFromDB = _unitOfWork.Product.Categories.Find(id);
            //if (ProductFromDB == null)
            //{
            //    return NotFound();
            //}
            if (product == null)
            {
                return NotFound();
            }
            //_unitOfWork.Product.Categories.Remove(ProductFromDB);
            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfuly";
            return RedirectToAction("Index");
        }
    }
}
