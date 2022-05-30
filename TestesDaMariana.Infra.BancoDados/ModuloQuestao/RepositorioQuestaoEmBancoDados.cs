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

namespace TestesDaMariana.Infra.BancoDados.ModuloQuestao
{
    public class RepositorioQuestaoEmBancoDados : IRepositorioQuestao
    {

        private const string enderecoBanco =
        "Data Source=(LocalDb)\\MSSQLLOCALDB;" +
        "Initial Catalog = GeradorTestes;" +
        "Integrated Security = True;" +
        "Pooling=False";

        #region Sql Queries
        private const string sqlInserir =
            @"INSERT INTO [TBQUESTAO] 
                (
                    [NUMERO_DISCIPLINA],
                    [NUMERO_MATERIA],
                    [ENUNCIADO],
                    [ALTERNATIVA_CORRETA]
	            )
	            VALUES
                (
                    @NUMERO_DISCIPLINA,
                    @NUMERO_MATERIA,
                    @ENUNCIADO,
                    @ALTERNATIVA_CORRETA
                );SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
            @"UPDATE [TBQUESTAO]	
		        SET
                    [NUMERO_DISCIPLINA] = @NUMERO_DISCIPLINA,
                    [NUMERO_MATERIA] = @NUMERO_MATERIA,
                    [ENUNCIADO] = @ENUNCIADO,
                    [ALTERNATIVA_CORRETA] = @ALTERNATIVA_CORRETA

		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlExcluir =
            @"DELETE FROM [TBQUESTAO]
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlSelecionarTodos =
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
                TBDISCIPLINA AS D ON NUMERO_DISCIPLINA = D.NUMERO
           INNER JOIN 
                TBMATERIA AS MT ON NUMERO_MATERIA = MT.NUMERO";

        private const string sqlSelecionarPorNumero =
                @"SELECT 
                Q.NUMERO, 
                Q.ENUNCIADO, 
                Q.ALTERNATIVA_CORRETA, 

                D.NUMERO AS NUMERO_DISCIPLINA, 
                D.NOME AS NOME_DISCIPLINA,

                M.NUMERO AS NUMERO_MATERIA,
                M.NOME AS NOME_MATERIA

                FROM 
                    TBQUESTAO AS Q

                INNER JOIN 
                       TBDISCIPLINA AS D 
                ON D.NUMERO = NUMERO_DISCIPLINA

                INNER JOIN TBMATERIA AS M
                ON
                     M.NUMERO = NUMERO_MATERIA
                WHERE
                    Q.NUMERO = @NUMERO";

        //ALTERNATIVAS
        private const string sqlInserirAlternativas =
           @"INSERT INTO [TBALTERNATIVA]
                (
		            [NUMERO_QUESTAO],
		            [LETRA],
		            [DESCRICAO]
	            )
                 VALUES
                (
		            @NUMERO_QUESTAO,
		            @LETRA,
		            @DESCRICAO
	            ); SELECT SCOPE_IDENTITY();";

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


        private const string sqlExcluirAlternativa =
          @"DELETE FROM [TBALTERNATIVA]
		        WHERE
			        [NUMERO_QUESTAO] = @NUMERO_QUESTAO";



        #endregion

        #region QUESTAO

        public ValidationResult Inserir(Questao novoRegistro)
        {
            var resultadoValidacao = Validar(novoRegistro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;


            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosQuestao(novoRegistro, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            novoRegistro.Numero = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Editar(Questao registro)
        {
            var resultadoValidacao = Validar(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosQuestao(registro, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            AdicionarAlternativas(registro, registro.Alternativas);

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Questao registro)
        {
            ExcluirAlternativa(registro);

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

        public Questao SelecionarPorNumero(int numero)
        {

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("NUMERO", numero);

            conexaoComBanco.Open();
            SqlDataReader leitorQuestao = comandoSelecao.ExecuteReader();

            Questao questao = null;

            if (leitorQuestao.Read())
                questao = ConverterParaQuestao(leitorQuestao);

            conexaoComBanco.Close();

            CarregarAlternativas(questao);

            return questao;
        }

        public List<Questao> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorQuestao = comandoSelecao.ExecuteReader();

            List<Questao> questoes = new List<Questao>();

            while (leitorQuestao.Read())
            {
                Questao questao = ConverterParaQuestao(leitorQuestao);
                CarregarAlternativas(questao);

                questoes.Add(questao);
            }

            conexaoComBanco.Close();

            return questoes;
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

        private static void ConfigurarParametrosQuestao(Questao novaQuestao, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NUMERO", novaQuestao.Numero);
            comando.Parameters.AddWithValue("NUMERO_DISCIPLINA", novaQuestao.Disciplina.Numero);
            comando.Parameters.AddWithValue("NUMERO_MATERIA", novaQuestao.Materia.Numero);
            comando.Parameters.AddWithValue("ENUNCIADO", novaQuestao.Enunciado);
            comando.Parameters.AddWithValue("ALTERNATIVA_CORRETA", novaQuestao.AlternativaCorreta);

        }
        public AbstractValidator<Questao> ObterValidador()
        {
            return new ValidadorQuestao();

        }

        private ValidationResult Validar(Questao registro)
        {
            var validator = ObterValidador();

            var resultadoValidacao = validator.Validate(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            var nomeEncontrado = SelecionarTodos()
               .Select(x => x.Enunciado.ToLower())
               .Contains(registro.Enunciado.ToLower());

            if (nomeEncontrado && registro.Numero == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Enunciado já está cadastrado"));

            return resultadoValidacao;
        }

        #endregion

        #region ALTERNATIVA
        public void AdicionarAlternativas(Questao questao, List<Alternativa> alternativas)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);
            conexaoComBanco.Open();

            foreach (Alternativa a in alternativas)
            {
                bool alternativaAdicionada = questao.AdicionarAlternativa(a);

                if (alternativaAdicionada)
                {
                    SqlCommand comandoInsercao = new SqlCommand(sqlInserirAlternativas, conexaoComBanco);

                    ConfigurarParametrosAlternativa(a, comandoInsercao);
                    var id = comandoInsercao.ExecuteScalar();
                    a.Numero = Convert.ToInt32(id);
                }
            }

            conexaoComBanco.Close();

        }

        private void ConfigurarParametrosAlternativa(Alternativa alternativa, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NUMERO", alternativa.Numero);
            comando.Parameters.AddWithValue("LETRA", alternativa.Letra);
            comando.Parameters.AddWithValue("DESCRICAO", alternativa.Descricao);
            comando.Parameters.AddWithValue("NUMERO_QUESTAO", alternativa.Questao.Numero);
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
     
        private Alternativa ConverterParaAlternativa(SqlDataReader leitorItemTarefa)
        {
            var numero = Convert.ToInt32(leitorItemTarefa["NUMERO"]);
            var letra = Convert.ToString(leitorItemTarefa["LETRA"]);
            var descricao = Convert.ToString(leitorItemTarefa["DESCRICAO"]);

            var alternativa = new Alternativa
            {
                Numero = numero,
                Letra = letra,
                Descricao = descricao
            };

            return alternativa;
        }


        public List<Questao> Sortear(Materia materia, int qtd)
        {
            int limite = 0;
            List<Questao> questoesSorteadas = new List<Questao>();
            List<Questao> questoesMateriaSelecionada = SelecionarTodos().Where(x => x.Materia.Numero.Equals(materia.Numero)).ToList();

            Random rdm = new Random();
            List<Questao> questoes = questoesMateriaSelecionada.OrderBy(item => rdm.Next()).ToList();

            foreach (Questao q in questoes)
            {
                questoesSorteadas.Add(q);
                limite++;
                if (limite == qtd)
                    break;
            }


            return questoesSorteadas;
        }

        public List<Questao> SortearQuestoesRecuperacao(Disciplina disciplina, int qtd)
        {
            int limite = 0;
            List<Questao> questoesSorteadas = new List<Questao>();
            List<Questao> questoesDisciplinaSelecionada = SelecionarTodos().Where(x => x.Disciplina.Numero.Equals(disciplina.Numero)).ToList();

            Random rdm = new Random();
            List<Questao> questoes = questoesDisciplinaSelecionada.OrderBy(item => rdm.Next()).ToList();

            foreach (Questao q in questoes)
            {
                questoesSorteadas.Add(q);
                limite++;
                if (limite == qtd)
                    break;
            }

            return questoesSorteadas;
        }


        private void ExcluirAlternativa(Questao q)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluirAlternativa, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("NUMERO_QUESTAO", q.Numero);

            conexaoComBanco.Open();
            comandoExclusao.ExecuteNonQuery();

            conexaoComBanco.Close();
        }


        #endregion

    }
}
