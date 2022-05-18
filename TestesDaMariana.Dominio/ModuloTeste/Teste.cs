using System;
using System.Collections.Generic;
using TestesDaMariana.Dominio.Compartilhado;
using TestesDaMariana.Dominio.ModuloDisciplina;
using TestesDaMariana.Dominio.ModuloMateria;
using TestesDaMariana.Dominio.ModuloQuestao;

namespace TestesDaMariana.Dominio.ModuloTeste
{
    public class Teste : EntidadeBase<Teste>, ICloneable
    {
        public string Titulo { get; set; }
        public Disciplina  Disciplina { get; set; }
        public Materia Materia { get; set; }
        public DateTime? dataCriacao { get; set; }

        private List<Questao> questoes = new List<Questao>();
        public List<Questao> Questoes { get => questoes; set => questoes = value; }

        public Teste()
        {
            dataCriacao = DateTime.Now;
        }

        public Teste(Disciplina d, Materia m, string t, List<Questao> questoes) : this()
        {
            Disciplina = d;
            Materia = m;
            Titulo = t;
            Questoes = questoes;
        }


        public override void Atualizar(Teste registro)
        {
            Titulo = registro.Titulo;
            Disciplina = registro.Disciplina;
            Materia = registro.Materia;
            Questoes = registro.Questoes;
        }

        private Teste(Teste registro)
        {
            Titulo = registro.Titulo;
            Disciplina = registro.Disciplina;
            Materia = registro.Materia;
            dataCriacao = DateTime.Now;
        }

        public object Clone()
        {
            return new Teste(this);
        }
    }
}
