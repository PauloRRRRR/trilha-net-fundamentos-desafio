using System;

namespace DesafioFundamentos.Models
{
    public class Menu
    {
        public static string ExibirMenu()
        {
            Console.Clear();
            Console.WriteLine("Digite a sua opção:");
            Console.WriteLine("1 - Cadastrar veículo");
            Console.WriteLine("2 - Remover veículo");
            Console.WriteLine("3 - Listar veículos");
            Console.WriteLine("4 - Encerrar");

            return Console.ReadLine();
        }
    }
}
