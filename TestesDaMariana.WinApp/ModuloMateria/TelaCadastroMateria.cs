using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TestesDaMariana.Dominio.ModuloDisciplina;
using TestesDaMariana.Dominio.ModuloMateria;
using TestesDaMariana.Infra.Compartilhado;

namespace TestesDaMariana.WinApp.ModuloMateria
{
    public partial class TelaCadastroMateria : Form
    {

        IRepositorioDisciplina repositorioDisciplina;
        private Materia materia;

        public TelaCadastroMateria(IRepositorioDisciplina repositorioDisciplina)
        {
            InitializeComponent();
            this.repositorioDisciplina = repositorioDisciplina;
            CarregarDisciplinas();
        }

        public Func<Materia, ValidationResult> GravarRegistro { get; set; }

        public Materia Materia
        {
            get
            {
                return materia;
            }
            set
            {
                materia = value;
                txtNomeMateria.Text = materia.Nome;
                comboBoxDisciplina.SelectedItem = materia.Disciplina;


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

        private void btnGravar_Click(object sender, EventArgs e)
        {
            materia.Nome = txtNomeMateria.Text;
            materia.Serie = ObtemSerie();
            materia.Disciplina = (Disciplina)comboBoxDisciplina.SelectedItem;

            var resultadoValidacao = GravarRegistro(Materia);
            if (resultadoValidacao.IsValid == false)
            {
                string erro = resultadoValidacao.Errors[0].ErrorMessage;

                TelaPrincipalForm.Instancia.AtualizarRodape(erro);

                DialogResult = DialogResult.None;
            }
        }

        private int ObtemSerie()
        {
            int n = 0;
            if (radioButton1serie.Checked)
                n = 1;
            else if (radioButton2serie.Checked)
                n = 2;

            return n;
        }

        #region rodapé
        private void TelaCadastroMateria_Load(object sender, EventArgs e)
        {
            TelaPrincipalForm.Instancia.AtualizarRodape("");

        }

        private void TelaCadastroMateria_FormClosing(object sender, FormClosingEventArgs e)
        {
            TelaPrincipalForm.Instancia.AtualizarRodape("");
        }
        #endregion
    }
}
