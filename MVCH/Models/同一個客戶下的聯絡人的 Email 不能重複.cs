using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCH.Models
{

    public class 同一個客戶下的聯絡人的_Email_不能重複Attribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                using (客戶資料Entities db = new 客戶資料Entities())
                {
                    var contact = (客戶聯絡人)validationContext.ObjectInstance;
                    var customer = db.客戶資料.FirstOrDefault(c => c.Id == contact.客戶Id && !c.已刪除);

                    if (customer == null)
                        return new ValidationResult("該客戶不存在!");

                    if (customer.客戶聯絡人.Any(c => c.Email == value.ToString().Trim()))
                    {
                        return new ValidationResult(string.Format("{0}下已存在 Email 為 {1} 的聯絡人，請另外使用別的 Email！", customer.客戶名稱, value));
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}