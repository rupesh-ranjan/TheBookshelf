﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rupesh.DataAccess.Repository.IRepository;
using Rupesh.Models;
using Rupesh.Models.ViewModels;
using Rupesh.Utility;
using System.Data;

namespace RupeshWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return View(objCompanyList);
        }
        public IActionResult Upsert(int? id)
        {
            
            if (id == null || id == 0)
            {
                // Create
                return View(new Company());
            } else
            {
                // Update
                Company company = _unitOfWork.Company.Get(u => u.Id == id);
                return View(company);
            }
        }
        [HttpPost]
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)
            {
                if (company.Id == 0)
                    _unitOfWork.Company.Add(company);
                else
                    _unitOfWork.Company.Update(company);
                _unitOfWork.Save();
                TempData["success"] = "Company created successfuly";
                return RedirectToAction("Index");
            } else
            { 
                return View(company);
            }
        }

        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var companyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
            if (companyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            
            _unitOfWork.Company.Remove(companyToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Success" });
        }
        #endregion

    }
}
