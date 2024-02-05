using Microsoft.VisualStudio.TestTools.UnitTesting;
using DesafioFundamentos.Models;
using System.Collections.Generic;

[TestClass]
public class EstacionamentoTests
{
    private Estacionamento estacionamento;

    [TestInitialize]
    public void SetUp()
    {
        // Limpa a lista de veículos antes de cada teste
        ArmazenamentoVeiculos.LimparVeiculos();

        // Inicializa a lista com alguns veículos de teste
        List<Veiculo> veiculosIniciais = new List<Veiculo>
        {
            new Veiculo("ABC123"),
            new Veiculo("XYZ987")
            // Adicione mais veículos conforme necessário
        };

        // Salva a lista de veículos no arquivo
        ArmazenamentoVeiculos.SalvarVeiculos(veiculosIniciais);

        // Inicializa o estacionamento com os valores fornecidos
        estacionamento = new Estacionamento(5, 2);
    }

    [TestMethod]
    public void ListarVeiculos_ComVeiculos_MostraVeiculos()
    {
        // Act
        estacionamento.ListarVeiculos();

        // Assert
        StringAssert.Contains("Veículos estacionados:", Console.Out.ToString());
        StringAssert.Contains("Placa:", Console.Out.ToString());
    }

    // Outros métodos de teste...

    private bool PlacaValida(string placa)
    {
        string formatoPlaca = "^[A-Z]{3}[0-9]{1}[A-Z]{1}[0-9]{2}$";
        return System.Text.RegularExpressions.Regex.IsMatch(placa, formatoPlaca);
    }
}
