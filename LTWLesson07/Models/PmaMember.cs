using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LTWLesson07.Models
{
    /// <summary>
    /// Model class member
    /// Athor: Mai Anh
    public class PmaMember
    {
        public int PmaMemberId { get; set; }

        [DisplayName("Tai Khoan")]
        [Required(ErrorMessage ="Tai khoan ko dc de trong")]
        [StringLength(20,MinimumLength =3, ErrorMessage ="Tai khoan co do dai trong khoang 3-20 ki tu")]
        public string? PmaUserName { get; set; }

        [DisplayName("Mat khau")]
        [StringLength(100, MinimumLength =8, ErrorMessage ="Mat khau toi thieu 8 ki tu")]
        public string? PmaPassword { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email ko dc de trong")]
        [DataType(DataType.EmailAddress)]
        public string? PmaEmail { get; set; }

        [DisplayName("So dien thoai")]
        [Required(ErrorMessage = "Ban chua nhap dien thoai")]
        [RegularExpression(@"^0\d{9,9}", ErrorMessage ="Dien thoai sai dinh dang, bat dau bang so 0")]
        public string PmaPhone { get; set; }
    }
}
