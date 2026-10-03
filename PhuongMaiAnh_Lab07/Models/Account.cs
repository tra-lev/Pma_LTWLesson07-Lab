using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PhuongMaiAnh_Lab07.Models
{
    public class Account
    {
        public int AccountId { get; set; }

        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Họ và tên ko được để trống")]
        [MinLength(6, ErrorMessage = "Họ tên ít nhất là 6 kí tự")]
        [MaxLength(20, ErrorMessage = "Họ và tên tối đa 20 kí tự")]
        public string FullName { get; set; }

        [Display(Name = "Địa chỉ email")]
        [Required(ErrorMessage = "Địa chỉ email ko được để trống")]
        [EmailAddress(ErrorMessage = "Địa chỉ email ko đúng định dạng")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        [DataType(DataType.PhoneNumber)]
        [Remote(action:"VerifyPhone", controller:"Account")]
        [Required(ErrorMessage = "Số điện thoại ko được để trống")]
        public string Phone { get; set; }

        [Display(Name = "Địa chỉ thường trú")]
        [Required(ErrorMessage = "Địa chỉ ko được để trống")]
        [StringLength(35, ErrorMessage = "Địa chỉ ko vượt quá 35 kí tự")]
        public string Address { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public string Avatar { get; set; }

        [Display(Name = "Giới tính")]
        public string Gender { get; set; }

        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Link Facebook cá nhân")]
        [Required(ErrorMessage = "Link Facebook ko được để trống")]
        [Url(ErrorMessage = "Url phải đúng định dạng bao gồm http hoặc https, tên miền VD: https://facebook.com/itvnsoft")]
        public string Facebook { get; set; }

        [Display(Name = "Ngày sinh")]
        [Required(ErrorMessage = "Ngày sinh ko được để trống")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
    }
}