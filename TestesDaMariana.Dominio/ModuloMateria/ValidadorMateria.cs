using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestesDaMariana.Dominio.ModuloMateria
{
    public class ValidadorMateria : AbstractValidator<Materia>
    {
        public ValidadorMateria()
        {
            RuleFor(x => x.Disciplina)
              .NotNull().NotEmpty();

            RuleFor(x => x.Nome)
              .NotNull().NotEmpty();

            RuleFor(x => x.Serie)
              .NotNull().NotEmpty();
        }
    }
}
