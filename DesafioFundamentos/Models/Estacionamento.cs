using System;
using System.Collections.Generic;
using System.Linq;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<Veiculo> veiculos = new List<Veiculo>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public void AdicionarVeiculo()
        {
            string placa;

            do
            {
                Console.WriteLine("Digite a placa do veículo para estacionar:");
                placa = Console.ReadLine();

                // Transforma todas as letras em maiúsculas
                placa = placa.ToUpper();

                // Verifica se a placa está no formato correto
                if (!PlacaValida(placa))
                {
                    Console.WriteLine("Placa inválida. Certifique-se de que está no formato correto (ABC1D23).");
                }
                else if (string.IsNullOrWhiteSpace(placa))
                {
                    Console.WriteLine("O valor da placa não pode ser vazio.");
                }
                else
                {
                    // Transforma todas as letras em maiúsculas
                    placa = placa.ToUpper();
                    veiculos.Add(new Veiculo(placa));
                    Console.WriteLine("Veículo cadastrado com sucesso!");
                }
            } while (!PlacaValida(placa) || string.IsNullOrWhiteSpace(placa));
        }


        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");
            string placa = Console.ReadLine();

            // Transforma todas as letras em maiúsculas
            placa = placa.ToUpper();

            Veiculo veiculoRemover = veiculos.FirstOrDefault(v => v.Placa.ToUpper() == placa.ToUpper());

            if (veiculoRemover != null)
            {
                Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");

                int horas = 0;

                while (!int.TryParse(Console.ReadLine(), out horas) || horas < 0)
                {
                    Console.WriteLine("Número inválido, tente novamente");
                }

                decimal valorTotal = CalcularValorTotal(horas);

                veiculos.Remove(veiculoRemover);
                Console.WriteLine($"Veículo com placa {placa} removido com sucesso.");
                Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: R$ {valorTotal}");
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
            }
        }

        private decimal CalcularValorTotal(int horas)
        {
            return precoInicial + precoPorHora * horas;
        }

        public void ListarVeiculos()
        {
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                foreach (Veiculo veiculo in veiculos)
                {
                    Console.WriteLine($"Placa: {veiculo.Placa}");
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }

        // Método para verificar o formato da placa
        private bool PlacaValida(string placa)
        {
            string formatoPlaca = "^[A-Z]{3}[0-9]{1}[A-Z]{1}[0-9]{2}$";
            return System.Text.RegularExpressions.Regex.IsMatch(placa, formatoPlaca);
        }
    }
}
