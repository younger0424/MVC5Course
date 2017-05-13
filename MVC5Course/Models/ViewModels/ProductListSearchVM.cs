using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC5Course.Models.ViewModels
{
    public class ProductListSearchVM : IValidatableObject
    {
        public ProductListSearchVM()
        {
            Stock_S = 0;
            Stock_E = 9999;
        }

        public string q { get; set; }
        public int Stock_S { get; set; }
        public int Stock_E { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            if (this.Stock_E < this.Stock_S)
            {
              yield return new ValidationResult("庫存資料篩選條件錯誤", new string[] { "Stock_S", "Stock_E" });
            }
        }
    }
}