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

        public List<Veiculo> Veiculos
        {
            get { return veiculos; }
        }

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            if (precoInicial < 0 || precoPorHora < 0)
            {
                throw new ArgumentException("Os preços não podem ser negativos.");
            }

            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
            this.veiculos = ArmazenamentoVeiculos.CarregarVeiculos();
        }

        public void AdicionarVeiculo()
        {
            do
            {
                Console.WriteLine("Digite a placa do veículo para estacionar (ou 'SAIR' para cancelar):");
                string placa = Console.ReadLine().ToUpper();

                if (placa == "SAIR")
                {
                    Console.WriteLine("Adição de veículo cancelada.");
                    return;
                }

                if (VeiculoExisteNoArquivo(placa))
                {
                    Console.WriteLine("Veículo já está estacionado. Adição cancelada.");
                    return;
                }

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
                    veiculos.Add(new Veiculo(placa));
                    Console.WriteLine("Veículo cadastrado com sucesso!");

                    // Atualizar o arquivo após a adição do veículo
                    ArmazenamentoVeiculos.SalvarVeiculos(veiculos);
                    break; // Sair do loop após adicionar com sucesso
                }
            } while (true); // Loop infinito até que uma placa válida seja fornecida
        }


        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");
            string placa = Console.ReadLine().ToUpper();

            Veiculo veiculoRemover = veiculos.FirstOrDefault(v => v.Placa.ToUpper() == placa.ToUpper());

            if (veiculoRemover != null)
            {

                int horas = 0;

                while (true)
                {
                    Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");

                    if (int.TryParse(Console.ReadLine(), out int horasAux) && horas >= 0)
                    {
                        decimal valorTotalHoras = CalcularValorTotal(horasAux);
                        veiculos.Remove(veiculoRemover);
                        Console.WriteLine($"Veículo com placa {placa} removido com sucesso.");
                        Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: R$ {valorTotalHoras}");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Número inválido, tente novamente.");
                    }
                }

                // Atualizar o arquivo após a remoção do veículo
                ArmazenamentoVeiculos.SalvarVeiculos(veiculos);
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
            }
        }

        private bool VeiculoExisteNoArquivo(string placa)
        {
            List<Veiculo> veiculosNoArquivo = ArmazenamentoVeiculos.CarregarVeiculos();
            return veiculosNoArquivo.Any(v => v.Placa == placa);
        }

        private decimal CalcularValorTotal(int horas)
        {
            return precoInicial + precoPorHora * horas;
        }

        public string ListarVeiculos()
        {
            if (veiculos.Any())
            {
                string listaVeiculos = "Os veículos estacionados são:\n";
                foreach (Veiculo veiculo in veiculos)
                {
                    listaVeiculos += $"Placa: {veiculo.Placa}\n";
                }
                return listaVeiculos;
            }
            else
            {
                return "Não há veículos estacionados.\n";
            }
        }

        public void ImprimirListaVeiculosConsole()
        {
            Console.WriteLine(ListarVeiculos());
        }

        // Método para verificar o formato da placa
        private bool PlacaValida(string placa)
        {
            string formatoPlaca = "^[A-Z]{3}[0-9]{1}[A-Z]{1}[0-9]{2}$";
            return System.Text.RegularExpressions.Regex.IsMatch(placa, formatoPlaca);
        }
    }
}
