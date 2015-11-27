using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MVCH.Models
{

    public sealed class 手機號碼Attribute : DataTypeAttribute
    {
        private static Regex _regex = new Regex(@"\d{4}-\d{6}");

        public 手機號碼Attribute() : base(DataType.Text)
        {
            this.ErrorMessage = "手機號碼格式不符!";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            return _regex.IsMatch(value.ToString());
        }
    }
}