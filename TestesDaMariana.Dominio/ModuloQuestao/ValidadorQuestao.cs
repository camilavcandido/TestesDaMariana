using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestesDaMariana.Dominio.ModuloQuestao
{
    public class ValidadorQuestao : AbstractValidator<Questao>
    {
        public ValidadorQuestao()
        {
            RuleFor(x => x.Disciplina)
            .NotNull().NotEmpty();

            RuleFor(x => x.Materia)
            .NotNull().NotEmpty();

            RuleFor(x => x.Enunciado)
              .NotNull().NotEmpty().MinimumLength(10);

            RuleFor(x => x.AlternativaCorreta)
              .NotNull().NotEmpty();

        }
    }

}