using System.Globalization;
using System.Windows.Controls;

namespace Smokeball.Seo.Ui.ValidationRules
{
    public sealed class NonEmptyTextboxValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult(false, "Input must be non-empty");
            }

            return ValidationResult.ValidResult;
        }
    }
}
