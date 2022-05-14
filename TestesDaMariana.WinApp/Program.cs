
using System;
using System.Windows.Forms;
using TestesDaMariana.Infra.Compartilhado;
using TestesDaMariana.Infra.Compartilhado.Serializador;


namespace TestesDaMariana.WinApp
{
    internal static class Program
    {
        static ISerializador serializador = new SerializadorJsonDotnet();

        static DataContext context = new DataContext(serializador);
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TelaPrincipalForm(context));
            context.GravarDados();


        }
    }
}
