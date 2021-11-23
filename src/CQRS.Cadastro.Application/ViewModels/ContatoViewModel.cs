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
        [StringLength(2, ErrorMessage = "O campo {0} precisa ter {1} caracteres", MinimumLength = 2)]
        public string Ddd { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(9, ErrorMessage = "O campo {0} precisa ter {1} caracteres", MinimumLength = 9)]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email informado não é válido.")]
        public string Email { get; set; }
    }
}
