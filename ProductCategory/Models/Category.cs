using System.ComponentModel.DataAnnotations;

namespace ProductCategory.Models
{
    public class Category
    {
        [Display(Name = "Mã danh mục")]
        public int Id { get; set; }

        [Display(Name = "Tên danh mục")]
        [Required(ErrorMessage = "Vui lòng nhập {0}.")]
        [StringLength(150, MinimumLength = 6, ErrorMessage = "{0} phải có từ {2} đến {1} ký tự.")]
        public string Name { get; set; } = string.Empty;
    }
}
