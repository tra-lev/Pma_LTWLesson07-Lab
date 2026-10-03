using System.ComponentModel.DataAnnotations;

namespace ProductCategory.Validations
{
    // SalePrice phải nhỏ hơn Price ít nhất một tỉ lệ (mặc định 10%): SalePrice <= Price * (1 - minDiscount)
    public class SalePriceAttribute : ValidationAttribute
    {
        private readonly string _pricePropertyName;
        private readonly double _minDiscount;

        public SalePriceAttribute(string pricePropertyName, double minDiscount = 0.1)
        {
            _pricePropertyName = pricePropertyName;
            _minDiscount = minDiscount;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var priceProperty = validationContext.ObjectType.GetProperty(_pricePropertyName);
            if (priceProperty == null)
                return new ValidationResult($"Không tìm thấy thuộc tính {_pricePropertyName}.");

            var salePrice = Convert.ToDouble(value);
            var price = Convert.ToDouble(priceProperty.GetValue(validationContext.ObjectInstance));
            var maxSalePrice = price * (1 - _minDiscount);

            if (salePrice > maxSalePrice)
            {
                return new ValidationResult(ErrorMessage ??
                    $"{validationContext.DisplayName} phải nhỏ hơn giá chuẩn ít nhất {_minDiscount * 100}% (tối đa {maxSalePrice:N0}).");
            }

            return ValidationResult.Success;
        }
    }
}
