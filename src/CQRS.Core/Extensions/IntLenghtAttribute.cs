using System.ComponentModel.DataAnnotations;

namespace CQRS.Core.Extensions
{
    public class IntLenghtAttribute : ValidationAttribute
    {
        private int _valorMin { get; set; }
        private int _valorMax { get; set; }

        public IntLenghtAttribute(int valorMinimo, int valorMaximo)
        {
            _valorMin = valorMinimo;
            _valorMax = valorMaximo;

            ErrorMessage = "O tamanho de {0} deve ser entre {1} e {2}";
        }

        public override bool IsValid(object value)
        {
            if (value.ToString().Length < _valorMin || value.ToString().Length > _valorMin)
            {
                return false;
            }

            return true;
        }
    }
}
