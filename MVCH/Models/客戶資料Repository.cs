using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVCH.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(c => !c.已刪除);
        }

        public 客戶資料 Find(int? id)
        {
            return this.All().FirstOrDefault(c => c.Id == id);
        }

        public IQueryable<客戶資料> Search(string search, int? customerType)
        {
            var data = this.All();

            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(c => c.客戶名稱.Contains(search));
            }

            if (customerType.HasValue)
            {
                data = data.Where(c => c.客戶分類 == customerType);
            }

            return data;
        }
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}