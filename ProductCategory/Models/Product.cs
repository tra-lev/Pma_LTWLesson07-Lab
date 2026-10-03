using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ProductCategory.Validations;

namespace ProductCategory.Models
{
    public class Product
    {
        [Display(Name = "Mã sản phẩm")]
        public int Id { get; set; }

        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Vui lòng nhập {0}.")]
        [StringLength(150, MinimumLength = 6, ErrorMessage = "{0} phải có từ {2} đến {1} ký tự.")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Hình ảnh")]
        [Required(ErrorMessage = "Vui lòng chọn {0}.")]
        public string Image { get; set; } = string.Empty;

        [Display(Name = "Giá")]
        [Required(ErrorMessage = "Vui lòng nhập {0}.")]
        [DataType(DataType.Text)]
        [Range(100000, float.MaxValue, ErrorMessage = "{0} phải lớn hơn hoặc bằng 100.000.")]
        public float Price { get; set; }

        [Display(Name = "Giá khuyến mãi")]
        [Required(ErrorMessage = "Vui lòng nhập {0}.")]
        [Range(0, float.MaxValue, ErrorMessage = "{0} không được là số âm.")]
        [SalePrice("Price", 0.1)]
        public float SalePrice { get; set; }

        [Display(Name = "Mô tả")]
        [Required(ErrorMessage = "Vui lòng nhập {0}.")]
        [StringLength(1500, ErrorMessage = "{0} không được vượt quá {1} ký tự.")]
        [ForbiddenWords]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Danh mục")]
        [Required(ErrorMessage = "Vui lòng chọn {0}.")]
        public int CategoryId { get; set; }

        [Display(Name = "Danh mục")]
        [ValidateNever]
        public Category? Category { get; set; }
    }
}
