﻿using Rupesh.DataAccess.Data;
using Rupesh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupesh.DataAccess.Repository.IRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _dbContext;
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }

        public UnitOfWork(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
            Category = new CategoryRepository(_dbContext);
            Product = new ProductRepository(_dbContext);
            Company = new CompanyRepository(_dbContext);
            ShoppingCart = new ShoppingCartRepository(_dbContext);
            ApplicationUser = new ApplicationUserRepository(_dbContext);

        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
