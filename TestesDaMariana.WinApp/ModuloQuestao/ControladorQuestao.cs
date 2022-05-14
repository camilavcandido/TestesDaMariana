using System.Collections.Generic;
using System.Windows.Forms;
using TestesDaMariana.Dominio.ModuloDisciplina;
using TestesDaMariana.Dominio.ModuloMateria;
using TestesDaMariana.Dominio.ModuloQuestao;
using TestesDaMariana.WinApp.Compartilhado;

namespace TestesDaMariana.WinApp.ModuloQuestao
{
    public class ControladorQuestao : ControladorBase
    {
        IRepositorioQuestao repositorioQuestao;
        IRepositorioDisciplina repositorioDisciplina;
        IRepositorioMateria repositorioMateria;

        TabelaQuestaoControl tabelaQuestaoControl;

        public ControladorQuestao(IRepositorioQuestao repositorioQuestao, IRepositorioDisciplina repositorioDisciplina, IRepositorioMateria repositorioMateria)
        {
            this.repositorioQuestao = repositorioQuestao;
            this.repositorioDisciplina = repositorioDisciplina;
            this.repositorioMateria = repositorioMateria;
        }

        public override void VisualizarDetalhes()
        {
            Questao questaoSelecionada = ObtemQuestaoSelecionada();

            if (questaoSelecionada == null)
            {
                MessageBox.Show("Selecione uma questão primeiro",
                "Visualização de Questões", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaVisualizacaoQuestaoForm tela = new TelaVisualizacaoQuestaoForm(questaoSelecionada);
            tela.Show();
        }

        public override void Inserir()
        {
            TelaCadastroQuestao tela = new TelaCadastroQuestao(repositorioDisciplina, repositorioMateria);
            tela.Questao = new Questao();

            tela.GravarRegistro = repositorioQuestao.Inserir;
          
            DialogResult resultado = tela.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                List<Alternativa> alternativas = tela.AlternativasAdicionadas;
                repositorioQuestao.AdicionarAlternativas(tela.Questao, alternativas);
                CarregarQuestoes();
            }
        }
        public override void Editar()
        {
            Questao questaoSelecionada = ObtemQuestaoSelecionada();

            if (questaoSelecionada == null)
            {
                MessageBox.Show("Selecione uma questão primeiro",
                "Edição de Questões", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaCadastroQuestao tela = new TelaCadastroQuestao(repositorioDisciplina, repositorioMateria);

            tela.Questao = questaoSelecionada;

            tela.GravarRegistro = repositorioQuestao.Editar;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                List<Alternativa> alternativas = tela.AlternativasAdicionadas;
                repositorioQuestao.AdicionarAlternativas(tela.Questao, alternativas);
                CarregarQuestoes();
            }
        }

        public override void Excluir()
        {
            Questao questaoSelecionada = ObtemQuestaoSelecionada();

            if (questaoSelecionada == null)
            {
                MessageBox.Show("Selecione uma questão primeiro",
                "Exclusão de Questões", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir a questão?",
                "Exclusão de Questões", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                repositorioQuestao.Excluir(questaoSelecionada);
                CarregarQuestoes();
            }
        }

        public override ConfiguracaoToolboxBase ObtemConfiguracaoToolbox()
        {
            return new ConfiguracaoToolboxQuestao();
        }

        public override UserControl ObtemListagem()
        {
            tabelaQuestaoControl = new TabelaQuestaoControl();

            CarregarQuestoes();

            return tabelaQuestaoControl;
        }

        private void CarregarQuestoes()
        {
            List<Questao> questoes = repositorioQuestao.SelecionarTodos();
            tabelaQuestaoControl.AtualizarRegistros(questoes);
            TelaPrincipalForm.Instancia.AtualizarRodape($"Visualizando Questões");
        }

        private Questao ObtemQuestaoSelecionada()
        {
            var numero = tabelaQuestaoControl.ObtemNumeroQuestaoSelecionada();

            return repositorioQuestao.SelecionarPorNumero(numero);
        }
    }
}
