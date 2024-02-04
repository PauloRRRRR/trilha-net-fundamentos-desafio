using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace DesafioFundamentos.Models
{
    public static class ArmazenamentoVeiculos
    {
        private static readonly string CaminhoArquivo = "veiculos.json";

        public static List<Veiculo> CarregarVeiculos()
        {
            if (File.Exists(CaminhoArquivo))
            {
                string json = File.ReadAllText(CaminhoArquivo);
                return JsonSerializer.Deserialize<List<Veiculo>>(json);
            }

            return new List<Veiculo>();
        }

        public static void SalvarVeiculos(List<Veiculo> veiculos)
        {
            string json = JsonSerializer.Serialize(veiculos);
            File.WriteAllText(CaminhoArquivo, json);
        }

        // Métodos adicionais para interação direta com a lista
        public static void AdicionarVeiculo(Veiculo veiculo)
        {
            List<Veiculo> veiculos = CarregarVeiculos();
            veiculos.Add(veiculo);
            SalvarVeiculos(veiculos);
        }

        public static void LimparVeiculos()
        {
            File.Delete(CaminhoArquivo);
        }
    }
}
