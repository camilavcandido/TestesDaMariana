using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TestesDaMariana.Infra.BancoDados.ModuloDisciplina;
using TestesDaMariana.Infra.BancoDados.ModuloMateria;
using TestesDaMariana.Infra.BancoDados.ModuloQuestao;
using TestesDaMariana.Infra.Compartilhado;
using TestesDaMariana.Infra.Repositorios;
using TestesDaMariana.WinApp.Compartilhado;
using TestesDaMariana.WinApp.ModuloDisciplina;
using TestesDaMariana.WinApp.ModuloMateria;
using TestesDaMariana.WinApp.ModuloQuestao;
using TestesDaMariana.WinApp.ModuloTeste;

namespace TestesDaMariana.WinApp
{
    public partial class TelaPrincipalForm : Form
    {
        private ControladorBase controlador;
        private Dictionary<string, ControladorBase> controladores;
        private DataContext dataContext;
        public TelaPrincipalForm(DataContext dataContext)
        {
            InitializeComponent();
            Instancia = this;
            txtRodape.Text = string.Empty;
            this.dataContext = dataContext;
            InicializarControladores();
        }

        public static TelaPrincipalForm Instancia
        {
            get;
            private set;
        }

        public void AtualizarRodape(string mensagem)
        {
            txtRodape.Text = mensagem;
        }


        private void InicializarControladores()
        {
            var repositorioDisciplina = new RepositorioDisciplinaEmBancoDados();
            var repositorioMateria = new RepositorioMateriaEmBancoDados();
            var repositorioQuestao = new RepositorioQuestaoEmBancoDados();
            var repositorioTeste = new RepositorioTesteEmArquivo(dataContext);
            
            controladores = new Dictionary<string, ControladorBase>();
            controladores.Add("Disciplinas", new ControladorDisciplina(repositorioDisciplina, repositorioMateria));
            controladores.Add("Matérias", new ControladorMateria(repositorioMateria, repositorioDisciplina));
            controladores.Add("Questões", new ControladorQuestao(repositorioQuestao, repositorioDisciplina, repositorioMateria, repositorioTeste));
            controladores.Add("Testes", new ControladorTeste(repositorioTeste, repositorioDisciplina, repositorioMateria, repositorioQuestao));
        
        }

        #region botoes

        private void btnInserir_Click(object sender, EventArgs e)
        {
            controlador.Inserir();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            controlador.Editar();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            controlador.Excluir();
        }
        private void btnVisualizarDetalhes_Click(object sender, EventArgs e)
        {
            controlador.VisualizarDetalhes();
        }

        private void btnDuplicar_Click(object sender, EventArgs e)
        {
            ((ControladorTeste)controlador).Duplicar();
        }

        private void btnGerarPdf_Click(object sender, EventArgs e)
        {
            ((ControladorTeste)controlador).GerarPdf();
        }

        #endregion

        #region menu item
        private void disciplinasMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);
        }

        private void materiaMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);
        }

        private void questoesMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);

        }

        private void testesMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);

        }
        #endregion

        private void ConfigurarTelaPrincipal(ToolStripMenuItem opcaoSelecionada)
        {
            var tipo = opcaoSelecionada.Text;

            controlador = controladores[tipo];

            ConfigurarToolbox();

            ConfigurarListagem();
        }

        private void ConfigurarToolbox()
        {
            ConfiguracaoToolboxBase configuracao = controlador.ObtemConfiguracaoToolbox();

          

            if (configuracao != null)
            {

                toolStrip.Visible = true;

                labelTipoCadastro.Text = configuracao.TipoCadastro;

               ConfigurarTooltips(configuracao);

               ConfigurarBotoes(configuracao);
            }

        }

        private void ConfigurarBotoes(ConfiguracaoToolboxBase configuracao)
        {
            btnInserir.Visible = configuracao.InserirHabilitado;
            btnEditar.Visible = configuracao.EditarHabilitado;
            btnExcluir.Visible = configuracao.ExcluirHabilitado;
            btnVisualizarDetalhes.Visible = configuracao.VisualizarDetalhesHabilitado;
            btnGerarPdf.Visible = configuracao.GerarPdfHabilitado;
            btnDuplicar.Visible = configuracao.DuplicarHabilitado;

        }

        private void ConfigurarTooltips(ConfiguracaoToolboxBase configuracao)
        {
            btnInserir.ToolTipText = configuracao.TooltipInserir;
            btnEditar.ToolTipText = configuracao.TooltipEditar;
            btnExcluir.ToolTipText = configuracao.TooltipExcluir;
            btnVisualizarDetalhes.ToolTipText = configuracao.TooltipVisualizarDetalhes;
            btnDuplicar.ToolTipText = configuracao.TooltipDuplicar;
            btnGerarPdf.ToolTipText = configuracao.TooltipGerarPdf;
        }

        private void ConfigurarListagem()
        {
            AtualizarRodape("");

            var listagemControl = controlador.ObtemListagem();

            panelRegistros.Controls.Clear();

            listagemControl.Dock = DockStyle.Fill;

            panelRegistros.Controls.Add(listagemControl);
        }


    }
}
