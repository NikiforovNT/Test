using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Globalization;

namespace Тестовое_задание.Base
{
    public class InspectorNumberValidationRule : ValidationRule
    {
        public InspectorNumberValidationRule()
        {
        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int number = 0;
            try
            {
                if (((string)value).Length > 0)
                    number = Int32.Parse((String)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Некорректный ввод: {e.Message}");
            }
            return ValidationResult.ValidResult;
        }
    }
}
