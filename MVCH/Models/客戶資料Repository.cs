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
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}