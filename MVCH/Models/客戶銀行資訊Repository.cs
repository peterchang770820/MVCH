using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
	
namespace MVCH.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All().Where(b => !b.已刪除);
        }

        public 客戶銀行資訊 Find(int? id)
        {
            return this.All().FirstOrDefault(b => b.Id == id);
        }

        public IQueryable<客戶銀行資訊> Search(string search)
        {
            var data = this.All();

            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(b => b.銀行名稱.Contains(search));
            }
            data = data.Include(b => b.客戶資料);

            return data;
        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}