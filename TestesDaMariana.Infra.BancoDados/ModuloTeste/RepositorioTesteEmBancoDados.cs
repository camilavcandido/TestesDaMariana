using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestesDaMariana.Dominio.ModuloDisciplina;
using TestesDaMariana.Dominio.ModuloMateria;
using TestesDaMariana.Dominio.ModuloQuestao;
using TestesDaMariana.Dominio.ModuloTeste;

namespace TestesDaMariana.Infra.BancoDados.ModuloTeste
{
    public class RepositorioTesteEmBancoDados : IRepositorioTeste
    {
        private const string enderecoBanco =
            "Data Source=(LocalDb)\\MSSQLLOCALDB;" +
            "Initial Catalog = GeradorTestes;" +
            "Integrated Security = True;" +
            "Pooling=False";

        private const string sqlInserir =
             @"INSERT INTO [TBTESTE] 
                (
                    [NUMERO_DISCIPLINA],
                    [NUMERO_MATERIA],
                    [DATA_CRIACAO],
                    [TITULO]
	            )
	            VALUES
                (
                    @NUMERO_DISCIPLINA,
                    @NUMERO_MATERIA,
                    @DATA_CRIACAO,
                    @TITULO
                );SELECT SCOPE_IDENTITY();";


        private const string sqlExcluir =
           @"DELETE FROM [TBTESTE]
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlInserirQuestao =
            @"INSERT INTO [TBTESTE_TBQUESTAO]
                (
                    [NUMERO_TESTE],
                    [NUMERO_QUESTAO]
	            )
	            VALUES
                (
                    @NUMERO_TESTE,
                    @NUMERO_QUESTAO
                )
            ";

        private const string sqlRemoverQuestaoTeste =
            @"
            DELETE FROM TBTESTE_TBQUESTAO
            WHERE NUMERO_TESTE = @NUMERO_TESTE
            ";

        private const string sqlSelecionarTodos =
         @"SELECT 
           T.NUMERO, 
           T.TITULO,

           D.NUMERO AS NUMERO_DISCIPLINA, 
           D.NOME AS NOME_DISCIPLINA,
           MT.NUMERO AS NUMERO_MATERIA, 
           MT.NOME AS NOME_MATERIA

           FROM 
               TBTESTE AS T
           INNER JOIN 
                TBDISCIPLINA AS D ON NUMERO_DISCIPLINA = D.NUMERO
           INNER JOIN 
                TBMATERIA AS MT ON NUMERO_MATERIA = MT.NUMERO";

        private const string sqlSelecionarPorNumero =
            @"SELECT 
           T.NUMERO, 
           T.TITULO,

           D.NUMERO AS NUMERO_DISCIPLINA, 
           D.NOME AS NOME_DISCIPLINA,
           MT.NUMERO AS NUMERO_MATERIA, 
           MT.NOME AS NOME_MATERIA

           FROM 
               TBTESTE AS T
           INNER JOIN 
                TBDISCIPLINA AS D ON NUMERO_DISCIPLINA = D.NUMERO
           INNER JOIN 
                TBMATERIA AS MT ON NUMERO_MATERIA = MT.NUMERO
               WHERE T.NUMERO = @NUMERO
              ";
        private const string sqlSelecionarQuestao =
        @"SELECT 
           Q.NUMERO, 
           Q.ENUNCIADO,
           Q.ALTERNATIVA_CORRETA,

           D.NUMERO AS NUMERO_DISCIPLINA, 
           D.NOME AS NOME_DISCIPLINA,
           MT.NUMERO AS NUMERO_MATERIA, 
           MT.NOME AS NOME_MATERIA

           FROM 
               TBQUESTAO AS Q
           INNER JOIN 
                TBTESTE_TBQUESTAO AS TQ ON TQ.NUMERO_QUESTAO = Q.NUMERO
           INNER JOIN 
                TBDISCIPLINA AS D ON NUMERO_DISCIPLINA = D.NUMERO
           INNER JOIN 
                TBMATERIA AS MT ON NUMERO_MATERIA = MT.NUMERO
           
            WHERE TQ.NUMERO_TESTE = @NUMERO";

        private const string sqlSelecionarAlternativas =
             @"SELECT 
	            [NUMERO],
                [NUMERO_QUESTAO],
                [LETRA],
                [DESCRICAO]
              FROM 
	            [TBALTERNATIVA]
              WHERE 
	            [NUMERO_QUESTAO] = @NUMERO_QUESTAO";

        public ValidationResult Inserir(Teste novoRegistro)
        {
            var validator = ObterValidador();

            var resultadoValidacao = validator.Validate(novoRegistro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;


            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosTeste(novoRegistro, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            novoRegistro.Numero = Convert.ToInt32(id);


            foreach(var item in novoRegistro.Questoes)
            {
                AdicionarQuestao(novoRegistro, item);
            }

            conexaoComBanco.Close();

            return resultadoValidacao;
        }
        public ValidationResult Excluir(Teste registro)
        {
            RemoverQuestaoTeste(registro);

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("NUMERO", registro.Numero);

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }
        public List<Teste> SelecionarTodos()
        {

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorTeste = comandoSelecao.ExecuteReader();

            List<Teste> testes = new List<Teste>();

            while (leitorTeste.Read())
            {
                Teste teste = ConverterParaTeste(leitorTeste);
                CarregarQuestoes(teste);
                testes.Add(teste);

            }

            conexaoComBanco.Close();

            return testes;
        }

        private void ConfigurarParametrosTeste(Teste novoRegistro, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NUMERO", novoRegistro.Numero);
            comando.Parameters.AddWithValue("NUMERO_DISCIPLINA", novoRegistro.Disciplina.Numero);
            comando.Parameters.AddWithValue("NUMERO_MATERIA", novoRegistro.Materia.Numero);
            comando.Parameters.AddWithValue("DATA_CRIACAO", novoRegistro.dataCriacao);
            comando.Parameters.AddWithValue("TITULO", novoRegistro.Titulo);
        }

        private Teste ConverterParaTeste(SqlDataReader leitorTeste)
        {
            int numero = Convert.ToInt32(leitorTeste["NUMERO"]);
            string titulo = Convert.ToString(leitorTeste["TITULO"]);

            int numeroDisciplina = Convert.ToInt32(leitorTeste["NUMERO_DISCIPLINA"]);
            string nomeDisciplina = Convert.ToString(leitorTeste["NOME_DISCIPLINA"]);

            int numeroMateria = Convert.ToInt32(leitorTeste["NUMERO_MATERIA"]);
            string nomeMateria = Convert.ToString(leitorTeste["NOME_MATERIA"]);

            var teste = new Teste
            {
                Numero = numero,
                Titulo = titulo,

                Disciplina = new Disciplina
                {
                    Numero = numeroDisciplina,
                    Nome = nomeDisciplina
                },

                Materia = new Materia
                {
                    Numero = numeroMateria,
                    Nome = nomeMateria
                }
            };

            return teste;
        }

        private void AdicionarQuestao(Teste teste, Questao questao)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercaoQuestao = new SqlCommand(sqlInserirQuestao, conexaoComBanco);

            comandoInsercaoQuestao.Parameters.AddWithValue("NUMERO_TESTE", teste.Numero);
            comandoInsercaoQuestao.Parameters.AddWithValue("NUMERO_QUESTAO", questao.Numero);

            conexaoComBanco.Open();
            comandoInsercaoQuestao.ExecuteNonQuery();
            conexaoComBanco.Close();

        }

        public AbstractValidator<Teste> ObterValidador()
        {
            return new ValidadorTeste();
        }


        public Teste SelecionarPorNumero(int numero)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("NUMERO", numero);

            conexaoComBanco.Open();
            SqlDataReader leitorTeste = comandoSelecao.ExecuteReader();

            Teste teste = null;
            if (leitorTeste.Read())
                teste = ConverterParaTeste(leitorTeste);

            conexaoComBanco.Close();

            CarregarQuestoes(teste);
            return teste;
        }

        public ValidationResult Editar(Teste registro)
        {
            throw new NotImplementedException();
        }

        public void RemoverQuestaoTeste(Teste registro)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlRemoverQuestaoTeste, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("NUMERO_TESTE", registro.Numero);

            conexaoComBanco.Open();
            comandoExclusao.ExecuteNonQuery();
            conexaoComBanco.Close();
        }

        private void CarregarQuestoes(Teste teste)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarQuestao, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("NUMERO", teste.Numero);

            conexaoComBanco.Open();
            SqlDataReader leitorQuestao = comandoSelecao.ExecuteReader();

            
            while (leitorQuestao.Read())
            {
                Questao q = ConverterParaQuestao(leitorQuestao);
                CarregarAlternativas(q);
                teste.Questoes.Add(q);
            }

            conexaoComBanco.Close();
        }

        private Questao ConverterParaQuestao(SqlDataReader leitorQuestao)
        {
            int numero = Convert.ToInt32(leitorQuestao["NUMERO"]);
            int numeroDisciplina = Convert.ToInt32(leitorQuestao["NUMERO_DISCIPLINA"]);
            int numeroMateria = Convert.ToInt32(leitorQuestao["NUMERO_MATERIA"]);
            string enunciado = Convert.ToString(leitorQuestao["ENUNCIADO"]);
            string alternativaCorreta = Convert.ToString(leitorQuestao["ALTERNATIVA_CORRETA"]);

            string nomeDisciplina = Convert.ToString(leitorQuestao["NOME_DISCIPLINA"]);
            string nomeMateria = Convert.ToString(leitorQuestao["NOME_MATERIA"]);

            var questao = new Questao
            {
                Numero = numero,
                Enunciado = enunciado,
                AlternativaCorreta = alternativaCorreta,

                Disciplina = new Disciplina
                {
                    Numero = numeroDisciplina,
                    Nome = nomeDisciplina
                },

                Materia = new Materia
                {
                    Numero = numeroMateria,
                    Nome = nomeMateria
                }

            };

            return questao;
        }


        private void CarregarAlternativas(Questao questao)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarAlternativas, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("NUMERO_QUESTAO", questao.Numero);

            conexaoComBanco.Open();
            SqlDataReader leitorAlternativa = comandoSelecao.ExecuteReader();


            while (leitorAlternativa.Read())
            {
                Alternativa a = ConverterParaAlternativa(leitorAlternativa);

                questao.AdicionarAlternativa(a);
            }

            conexaoComBanco.Close();
        }

        private Alternativa ConverterParaAlternativa(SqlDataReader leitorAlternativa)
        {
            var numero = Convert.ToInt32(leitorAlternativa["NUMERO"]);
            var letra = Convert.ToString(leitorAlternativa["LETRA"]);
            var descricao = Convert.ToString(leitorAlternativa["DESCRICAO"]);

            var alternativa = new Alternativa
            {
                Numero = numero,
                Letra = letra,
                Descricao = descricao
            };

            return alternativa;
        }
    }
}
