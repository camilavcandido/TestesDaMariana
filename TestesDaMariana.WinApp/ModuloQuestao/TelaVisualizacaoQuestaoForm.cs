using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestesDaMariana.Dominio.ModuloQuestao;

namespace TestesDaMariana.WinApp.ModuloQuestao
{
    public partial class TelaVisualizacaoQuestaoForm : Form
    {
        private Questao questao;
        public TelaVisualizacaoQuestaoForm(Questao questao)
        {
            InitializeComponent();
            this.questao = questao;
            txtDisciplina.Text = questao.Disciplina.Nome;
            txtMateria.Text = questao.Materia.Nome;
            txtEnunciado.Text = questao.Enunciado;
            CarregarAlternativas();
        }

        private void CarregarAlternativas()
        {
            List<Alternativa> lisAlternativas = questao.Alternativas.ToList();

            foreach (Alternativa a in lisAlternativas)
            {

                if (a.Letra.Equals(questao.AlternativaCorreta))
                {
                    checkedListBoxAlternativas.Items.Add(a);
                    int index = checkedListBoxAlternativas.Items.IndexOf(a);
                    checkedListBoxAlternativas.SetItemChecked(index, true);
                }
                else
                    checkedListBoxAlternativas.Items.Add(a);

            }
        }
        
    }
}
