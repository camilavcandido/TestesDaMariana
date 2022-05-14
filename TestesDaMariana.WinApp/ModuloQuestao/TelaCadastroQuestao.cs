using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TestesDaMariana.Dominio.ModuloDisciplina;
using TestesDaMariana.Dominio.ModuloMateria;
using TestesDaMariana.Dominio.ModuloQuestao;

namespace TestesDaMariana.WinApp.ModuloQuestao
{
    public partial class TelaCadastroQuestao : Form
    {
        IRepositorioDisciplina repositorioDisciplina;
        IRepositorioMateria repositorioMateria;
        private Questao questao;

        public TelaCadastroQuestao(IRepositorioDisciplina repositorioDisciplina, IRepositorioMateria repositorioMateria)
        {
            InitializeComponent();
            this.repositorioDisciplina = repositorioDisciplina;
            this.repositorioMateria = repositorioMateria;
            CarregarDisciplinas();
        }
        public Func<Questao, ValidationResult> GravarRegistro { get; set; }

        public Questao Questao
        {
            get
            {
                return questao;
            }
            set
            {

                questao = value;
                txtEnunciado.Text = questao.Enunciado;
                comboBoxDisciplinas.SelectedItem = questao.Disciplina;
                comboBoxMaterias.SelectedItem = questao.Materia;
                comboBoxAlternativaCorreta.SelectedItem = questao.AlternativaCorreta;
                listAlternativas.Items.AddRange(questao.Alternativas.ToArray());
            }
        }

        public List<Alternativa> AlternativasAdicionadas
        {
            get
            {
                return listAlternativas.Items.Cast<Alternativa>().ToList();
            } set
            {

            }
        }


        private void btnGravar_Click(object sender, EventArgs e)
        {
            questao.Disciplina = (Disciplina)comboBoxDisciplinas.SelectedItem;
            questao.Materia = (Materia)comboBoxMaterias.SelectedItem;
            questao.Enunciado = txtEnunciado.Text.ToString();
            questao.AlternativaCorreta = (string)comboBoxAlternativaCorreta.SelectedItem;

            var resultadoValidacao = GravarRegistro(questao);
            if (resultadoValidacao.IsValid == false)
            {
                string erro = resultadoValidacao.Errors[0].ErrorMessage;

                TelaPrincipalForm.Instancia.AtualizarRodape(erro);

                DialogResult = DialogResult.None;
            }      

        }

        private void btnAddAlternativa_Click(object sender, EventArgs e)
        {
            List<string> letras = AlternativasAdicionadas.Select(x => x.Letra).ToList();

            List<string> descricoes = AlternativasAdicionadas.Select(x => x.Descricao).ToList();

            if (descricoes.Count == 0 || descricoes.Contains(txtDescricaoAlternativa.Text) == false)
            {
                if(letras.Contains(comboBoxLetraAlternativa.Text) == false)
                {
                    string letra = (string)comboBoxLetraAlternativa.SelectedItem;
                    string descricao = txtDescricaoAlternativa.Text;

                    Alternativa novaAlternativa = new Alternativa(letra, descricao);
                    listAlternativas.Items.Add(novaAlternativa);

                }

            }


        }

        private void btnExcluirAlternativa_Click(object sender, EventArgs e)
        {
            Alternativa alternativaSelecionada = (Alternativa)listAlternativas.SelectedItem;
              

            if (alternativaSelecionada == null)
            {
                MessageBox.Show("Selecione uma alternativa primeiro",
                "Exclusão de Alternativas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir a alternativa?",
                "Exclusão de Alternativas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                listAlternativas.Items.Remove(alternativaSelecionada);
            }

        }

        private void CarregarDisciplinas()
        {
            List<Disciplina> disciplinas = repositorioDisciplina.SelecionarTodos();
            foreach (Disciplina d in disciplinas)
            {
                comboBoxDisciplinas.Items.Add(d);
            }
        }
        private void comboBoxDisciplinas_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxMaterias.Items.Clear();
            List<Materia> materias = repositorioMateria.SelecionarTodos();
            foreach(Materia m in materias)
            {
                if(m.Disciplina == comboBoxDisciplinas.SelectedItem)
                {
                    comboBoxMaterias.Items.Add(m);
                }
            }

        }
    }
}
