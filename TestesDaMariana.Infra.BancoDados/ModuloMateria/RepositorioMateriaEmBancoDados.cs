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

namespace TestesDaMariana.Infra.BancoDados.ModuloMateria
{
    public class RepositorioMateriaEmBancoDados : IRepositorioMateria
    {
        private const string enderecoBanco =
            "Data Source=(LocalDb)\\MSSQLLOCALDB;" +
           "Initial Catalog = GeradorTestes;" +
           "Integrated Security = True;" +
           "Pooling=False";

        #region Sql Queries
        private const string sqlInserir =
            @"INSERT INTO [TBMATERIA] 
                (
                    [NOME],
                    [SERIE],
                    [DISCIPLINA]
	            )
	            VALUES
                (
                    @NOME,
                    @SERIE,
                    @DISCIPLINA
                );SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
            @"UPDATE [TBMATERIA]	
		        SET
			        [NOME] = @NOME,
                    [SERIE] = @SERIE,
                    [DISCIPLINA] = @DISCIPLINA
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlExcluir =
            @"DELETE FROM [TBMATERIA]
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlSelecionarTodos =
            @"SELECT 
            MT.NUMERO, 
            MT.NOME, 
            MT.SERIE, 

            D.NUMERO AS DISCIPLINA_NUMERO, 
            D.NOME AS DISCIPLINA_NOME

            FROM TBMATERIA AS MT
            INNER JOIN TBDISCIPLINA AS D 
            ON D.NUMERO = MT.DISCIPLINA";

        private const string sqlSelecionarPorNumero =
            @"SELECT 
                MT.NUMERO, 
                MT.NOME, 
                MT.SERIE, 

                D.NUMERO AS DISCIPLINA_NUMERO, 
                D.NOME AS DISCIPLINA_NOME

                FROM 
                    TBMATERIA AS MT
                INNER JOIN 
                       TBDISCIPLINA AS D 
                ON
                     D.NUMERO = MT.DISCIPLINA
                WHERE
                    MT.NUMERO = @NUMERO";

        private const string sqlInserirAlternativas =
            @"INSERT INTO [DBO].[TBALTERNATIVA]
                (
		            [NUMERO_QUESTAO]
		            [LETRA],
		            [DESCRICAO],
	            )
                 VALUES
                (
		            @NUMERO_QUESTAO 
		            @LETRA,
		            @DESCRICAO
	            ); SELECT SCOPE_IDENTITY();";

        #endregion

        public ValidationResult Inserir(Materia novaMateria)
        {

            var resultadoValidacao = Validar(novaMateria);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;


            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            comandoInsercao.Parameters.AddWithValue("DISCIPLINA_NUMERO", novaMateria.Disciplina.Numero);

            ConfigurarParametrosMateria(novaMateria, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            novaMateria.Numero = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;

        }

        private static void ConfigurarParametrosMateria(Materia novaMateria, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NUMERO", novaMateria.Numero);
            comando.Parameters.AddWithValue("NOME", novaMateria.Nome);
            comando.Parameters.AddWithValue("SERIE", novaMateria.Serie);
            comando.Parameters.AddWithValue("DISCIPLINA", novaMateria.Disciplina.Numero);

        }

        public ValidationResult Editar(Materia registro)
        {
            var resultadoValidacao = Validar(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosMateria(registro, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Materia registro)
        {
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

        public List<Materia> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorMateria = comandoSelecao.ExecuteReader();

            List<Materia> materias = new List<Materia>();

            while (leitorMateria.Read())
            {
                Materia materia = ConverterParaMateria(leitorMateria);

                materias.Add(materia);
            }

            conexaoComBanco.Close();

            return materias;
        } 
        private static Materia ConverterParaMateria(SqlDataReader leitorMateria)
        {
            int numero = Convert.ToInt32(leitorMateria["NUMERO"]);
            string nome = Convert.ToString(leitorMateria["NOME"]);
            int serie = Convert.ToInt32(leitorMateria["SERIE"]);

            int numeroDisciplina = Convert.ToInt32(leitorMateria["DISCIPLINA_NUMERO"]);
            string nomeDisciplina = Convert.ToString(leitorMateria["DISCIPLINA_NOME"]);

            var materia = new Materia
            {
                Numero = numero,
                Nome = nome,
                Serie = serie,

                Disciplina = new Disciplina
                {
                    Numero = numeroDisciplina,
                    Nome = nomeDisciplina
                }

            };

            return materia;
        }
        public Materia SelecionarPorNumero(int numero)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("NUMERO", numero);

            conexaoComBanco.Open();
            SqlDataReader leitorMateria = comandoSelecao.ExecuteReader();

            Materia materia = null;
            if (leitorMateria.Read())
                materia = ConverterParaMateria(leitorMateria);

            conexaoComBanco.Close();

            return materia;
        }

        private ValidationResult Validar(Materia registro)
        {
            var validator = ObterValidador();

            var resultadoValidacao = validator.Validate(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            var materias = SelecionarTodos();
            foreach (var m in materias)
            {
                if (m.Nome.ToLower() == registro.Nome.ToLower() && m.Serie == registro.Serie && registro.Numero == 0)
                    resultadoValidacao.Errors.Add(new ValidationFailure("", "Nome da matéria já está cadastrado com a série selecionada"));

            }

            return resultadoValidacao;
        }
        public AbstractValidator<Materia> ObterValidador()
        {
            return new ValidadorMateria();
        }



    }
}
