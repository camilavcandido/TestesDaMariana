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
using TestesDaMariana.Dominio.ModuloTeste;

namespace TestesDaMariana.WinApp.ModuloTeste
{
    public partial class TelaVisualizacaoTesteForm : Form
    {
        public TelaVisualizacaoTesteForm(Teste teste)
        {
            InitializeComponent();
            ExibirTeste(teste);
        }

        private void ExibirTeste(Teste teste)
        {
            txtDisciplina.Text = teste.Disciplina.Nome;
            txtMateria.Text = teste.Materia.Nome;
            txtTitulo.Text = teste.Titulo;


            foreach (var q in teste.Questoes)
            {
                List<Alternativa> ordenadas = q.Alternativas.OrderBy(x => x.Letra).ToList();

                listBoxQuestoes.Items.Add(q.Enunciado);
                foreach(var a in ordenadas)
                {        
                    listBoxQuestoes.Items.Add(a.ToString());
                }
            }
        }


    }
}
