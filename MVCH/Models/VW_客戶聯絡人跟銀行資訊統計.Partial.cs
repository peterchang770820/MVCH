namespace MVCH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(VW_客戶聯絡人跟銀行資訊統計MetaData))]
    public partial class VW_客戶聯絡人跟銀行資訊統計
    {
    }
    
    public partial class VW_客戶聯絡人跟銀行資訊統計MetaData
    {
        [Required]
        public int Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 客戶名稱 { get; set; }
        public Nullable<int> 聯絡人數量 { get; set; }
        public Nullable<int> 銀行資訊數量 { get; set; }
    }
}
