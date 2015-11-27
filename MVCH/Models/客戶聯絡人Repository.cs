using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace MVCH.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(c => !c.已刪除);
        }

        public 客戶聯絡人 Find(int? id)
        {
            return this.All().FirstOrDefault(c => c.Id == id);
        }

        public IQueryable<客戶聯絡人> Search(string search)
        {
            var data = this.All();

            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(c => c.姓名.Contains(search));
            }
            data = data.Include(客 => 客.客戶資料);

            return data;
        }
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}