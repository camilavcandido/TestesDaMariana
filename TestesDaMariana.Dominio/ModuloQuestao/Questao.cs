using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestesDaMariana.Dominio.Compartilhado;
using TestesDaMariana.Dominio.ModuloDisciplina;
using TestesDaMariana.Dominio.ModuloMateria;

namespace TestesDaMariana.Dominio.ModuloQuestao
{
    public class Questao : EntidadeBase<Questao>
    {
        public Disciplina Disciplina { get; set; }
        public Materia Materia { get; set; }
        public string Enunciado { get; set; }      
        public string AlternativaCorreta { get; set; }
        public List<Alternativa> Alternativas { get => alternativas; set => alternativas = value; }
        private List<Alternativa> alternativas = new List<Alternativa>();

        public Questao()
        {
        }

        public Questao(Disciplina disciplina, Materia materia, string enunciado, List<Alternativa> alternativas, string alternativaCorreta) : this()
        {
            Disciplina = disciplina;
            Materia = materia;
            Enunciado = enunciado;
            Alternativas = alternativas;
            AlternativaCorreta = alternativaCorreta;
        }

        public override void Atualizar(Questao registro)
        {
            this.Enunciado = registro.Enunciado;
            this.Materia = registro.Materia;
            this.AlternativaCorreta = registro.AlternativaCorreta;
            this.alternativas = registro.Alternativas;
        }

        public void AdicionarAlternativa(Alternativa alternativa)
        {
            if (Alternativas.Exists(x => x.Equals(alternativa)) == false)
                alternativas.Add(alternativa);
        }

        public override string ToString()
        {
            return $"Enunciado: {Enunciado}";
        }
    }
}
