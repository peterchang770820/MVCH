using System;
using System.Linq;
using System.Collections.Generic;
	
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
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}