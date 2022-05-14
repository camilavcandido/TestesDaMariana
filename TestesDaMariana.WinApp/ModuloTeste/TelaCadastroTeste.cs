using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestesDaMariana.Dominio.ModuloDisciplina;
using TestesDaMariana.Dominio.ModuloMateria;
using TestesDaMariana.Dominio.ModuloQuestao;
using TestesDaMariana.Dominio.ModuloTeste;

namespace TestesDaMariana.WinApp.ModuloTeste
{
    public partial class TelaCadastroTeste : Form
    {
        IRepositorioDisciplina repositorioDisciplina;
        IRepositorioMateria repositorioMateria;
        IRepositorioQuestao repositorioQuestao;
        private Teste teste;
        public TelaCadastroTeste(IRepositorioDisciplina repositorioDisciplina, IRepositorioMateria repositorioMateria, IRepositorioQuestao repositorioQuestao)
        {
            InitializeComponent();
            this.repositorioDisciplina = repositorioDisciplina;
            this.repositorioMateria = repositorioMateria;
            this.repositorioQuestao = repositorioQuestao;
            CarregarDisciplinas();
        }

        public Teste Teste 
        {
            get { return teste;  }
            set { 
                teste = value;
                txtTituloTeste.Text = teste.Titulo;
                comboBoxDisciplina.SelectedItem = teste.Disciplina;
                comboBoxMateria.SelectedItem = teste.Materia;
                listBoxQuestoes.Items.AddRange(teste.Questoes.ToArray());
            }

        }

        public Func<Teste, ValidationResult> GravarRegistro { get; set; }


        private void btnGravar_Click(object sender, EventArgs e)
        {
            
            teste.Titulo = txtTituloTeste.Text;
            teste.Disciplina = (Disciplina)comboBoxDisciplina.SelectedItem;
            teste.Materia = (Materia)comboBoxMateria.SelectedItem;
            teste.Questoes = QuestoesSorteadas;

            var resultadoValidacao = GravarRegistro(teste);
            if (resultadoValidacao.IsValid == false)
            {
                string erro = resultadoValidacao.Errors[0].ErrorMessage;

                TelaPrincipalForm.Instancia.AtualizarRodape(erro);

                DialogResult = DialogResult.None;
            }
        }


        public List<Questao> QuestoesSorteadas
        {
            get
            {
                return listBoxQuestoes.Items.Cast<Questao>().ToList();
            }
        }

        private void CarregarDisciplinas()
        {
            List<Disciplina> disciplinas = repositorioDisciplina.SelecionarTodos();
            foreach (Disciplina d in disciplinas)
            {
                comboBoxDisciplina.Items.Add(d);
            }
        }

        private void comboBoxDisciplina_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxMateria.Items.Clear();
            List<Materia> materias = repositorioMateria.SelecionarTodos();
            foreach (Materia m in materias)
            {
                if (m.Disciplina == comboBoxDisciplina.SelectedItem)
                {
                    comboBoxMateria.Items.Add(m);
                }
            }
        }

        private void btnSortearQuestoes_Click(object sender, EventArgs e)
        {
            listBoxQuestoes.Items.Clear();

            int qtdQuestoes = (int)numQuestoes.Value;
            Materia materiaSelecionada = (Materia)comboBoxMateria.SelectedItem;

            List<Questao> questoesSorteadas = repositorioQuestao.Sortear(materiaSelecionada, qtdQuestoes);
            foreach(Questao q in questoesSorteadas)
            {
                listBoxQuestoes.Items.Add(q);
            }
        }


    }
}
