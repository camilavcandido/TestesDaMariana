
namespace TestesDaMariana.Dominio.ModuloQuestao
{
    public class Alternativa
    {
        public string Letra { get; set; }
        public string Descricao { get; set; }

        public Alternativa()
        {

        }
                                     
        public Alternativa(string letra, string descricao) : this()
        {
            Letra = letra;
            Descricao = descricao;
        }


        public override string ToString()
        {
            return $"{Letra}) {Descricao}";
        }
    }
}