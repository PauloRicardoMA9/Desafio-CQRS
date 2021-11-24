using DocumentValidator;
using System.ComponentModel.DataAnnotations;

namespace CQRS.Core.Extensions
{
    public class CpfValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            var cpf = (string)value;
            return CpfValidation.Validate(cpf);
        }
    }
}
