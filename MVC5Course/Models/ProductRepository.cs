using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace MVC5Course.Models
{   
	public  class ProductRepository : EFRepository<Product>, IProductRepository
	{
        public override IQueryable<Product> All()
        {
            return base.All().Where(p => !p.Is刪除);
        }

        public IQueryable<Product> All(bool showAll)
        {
            if (showAll)
            {
                return base.All();
            }
            else
            {
                return this.All();
            }
        }

        public Product  Get單筆資料ByProductId(int id)
        {
            return All().FirstOrDefault(p => p.ProductId == id);
        }

        public IQueryable<Product> GetProduct所有資料(bool? Active = true, bool showAll = false)
        {
            IQueryable<Product> all = this.All();
            if (showAll)
            {
                all = base.All();
            }
            return All().Where(p => p.Active.Value == Active).OrderByDescending(p => p.ProductId).Take(10);
        }

        public void Update(Product product)
        {
            this.UnitOfWork.Context.Entry(product).State = EntityState.Modified;
        }

        public override void Delete(Product entity)
        {
            // base.Delete(entity);)
            //關閉驗證
            this.UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;
            entity.Is刪除 = true;
        }

    }

	public  interface IProductRepository : IRepository<Product>
	{

	}
}