using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestesDaMariana.Dominio.Compartilhado;

namespace TestesDaMariana.Dominio.ModuloDisciplina
{
    public class Disciplina : EntidadeBase<Disciplina>
    {
        public string Nome { get; set; }

        public Disciplina()
        {

        }

        public Disciplina(int n, string nome) : this()
        {
            Numero = n;
            Nome = nome;
        }

        public override void Atualizar(Disciplina registro)
        {
            this.Nome = registro.Nome;
        }

        public override string ToString()
        {
            return Nome;
        }

    }
}
