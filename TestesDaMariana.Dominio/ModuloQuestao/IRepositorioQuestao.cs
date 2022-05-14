using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestesDaMariana.Dominio.Compartilhado;
using TestesDaMariana.Dominio.ModuloMateria;

namespace TestesDaMariana.Dominio.ModuloQuestao
{
    public interface IRepositorioQuestao : IRepositorioBase<Questao>
    {
        void AdicionarAlternativas(Questao q, List<Alternativa> a);
         List<Questao> Sortear(Materia materia, int qtd);
    }
}
