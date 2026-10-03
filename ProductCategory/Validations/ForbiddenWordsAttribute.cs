using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ProductCategory.Validations
{
    // Không cho phép chứa các từ nhạy cảm (không phân biệt hoa thường)
    public class ForbiddenWordsAttribute : ValidationAttribute
    {
        // Sửa danh sách từ cấm tại đây
        private static readonly string[] ForbiddenWords = { "die", "admin", "fack" };

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var text = value as string;
            if (string.IsNullOrEmpty(text))
                return ValidationResult.Success;

            var found = ForbiddenWords
                .Where(w => Regex.IsMatch(text, $@"\b{Regex.Escape(w)}\b", RegexOptions.IgnoreCase))
                .ToList();

            if (found.Count > 0)
            {
                return new ValidationResult(
                    $"{validationContext.DisplayName} chứa từ bị cấm: {string.Join(", ", found)}. Vui lòng sửa lại.");
            }

            return ValidationResult.Success;
        }
    }
}
