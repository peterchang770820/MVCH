using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVCH.Models
{   
	public  class VW_客戶聯絡人跟銀行資訊統計Repository : EFRepository<VW_客戶聯絡人跟銀行資訊統計>, IVW_客戶聯絡人跟銀行資訊統計Repository
	{
        public override IQueryable<VW_客戶聯絡人跟銀行資訊統計> All()
        {
            return base.All().Where(c => !c.已刪除);
        }
    }

	public  interface IVW_客戶聯絡人跟銀行資訊統計Repository : IRepository<VW_客戶聯絡人跟銀行資訊統計>
	{

	}
}