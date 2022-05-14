using FluentValidation;

namespace TestesDaMariana.Dominio.ModuloTeste
{
    public class ValidadorTeste : AbstractValidator<Teste>
    {
        public ValidadorTeste()
        {
            RuleFor(x => x.Titulo)
                  .NotNull().NotEmpty();

            RuleFor(x => x.Disciplina)
             .NotNull().NotEmpty();

            RuleFor(x => x.Materia)
             .NotNull().NotEmpty();

        }
    }
}
