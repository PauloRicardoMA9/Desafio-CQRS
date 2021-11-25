using CQRS.Core.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace CQRS.Cadastro.Application.ViewModels
{
    public class ContatoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ClienteId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [IntLenght(2, 2, ErrorMessage = "O campo {0} deve conter {1} dígitos.")]
        public int Ddd { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [IntLenght(9, 9, ErrorMessage = "O campo {0} deve conter {1} dígitos.")]
        public int Telefone { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email informado não é válido.")]
        public string Email { get; set; }
    }
}
