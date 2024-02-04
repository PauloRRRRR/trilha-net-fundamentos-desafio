using Microsoft.VisualStudio.TestTools.UnitTesting;
using DesafioFundamentos.Models;

[TestClass]
public class EstacionamentoTests
{
    private Estacionamento estacionamento;

    [TestInitialize]
    public void SetUp()
    {
        estacionamento = new Estacionamento(5, 2); // Ajuste os parâmetros conforme necessário
    }

    [TestMethod]
    public void AdicionarVeiculo_PlacaValida_VeiculoAdicionadoComSucesso()
    {
        // Arrange
        string placa = "ABC1D23";
        Console.SetIn(new StringReader(placa)); // Simula a entrada do usuário

        // Act
        estacionamento.AdicionarVeiculo();

        // Assert
        Assert.AreEqual(1, estacionamento.Veiculos.Count);
        Assert.AreEqual(placa.ToUpper(), estacionamento.Veiculos[0].Placa);
    }

    [TestMethod]
    public void AdicionarVeiculo_PlacaInvalida_PromptReapresentado()
    {
        // Arrange
        string placaInvalida = "XYZ"; // Placa inválida
        string placaValida = "ABC1D23";
        Console.SetIn(new StringReader(placaInvalida + Environment.NewLine + placaValida)); // Simula a entrada do usuário

        // Act
        estacionamento.AdicionarVeiculo();

        // Assert
        Assert.AreEqual(1, estacionamento.Veiculos.Count);
        Assert.AreEqual(placaValida.ToUpper(), estacionamento.Veiculos[0].Placa);
    }

    [TestMethod]
    public void ListarVeiculos_SemVeiculos_NaoMostraNada()
    {
        // Act
        estacionamento.ListarVeiculos();

        // Assert
        StringAssert.Contains("Não há veículos estacionados.", Console.Out.ToString());
    }

    [TestMethod]
    public void ListarVeiculos_ComVeiculos_MostraVeiculos()
    {
        // Arrange
        estacionamento.AdicionarVeiculo(); // Adicione veículos conforme necessário

        // Act
        estacionamento.ListarVeiculos();

        // Assert
        StringAssert.Contains("Veículos estacionados:", Console.Out.ToString());
        StringAssert.Contains("Placa:", Console.Out.ToString());
    }

    // Outros métodos de teste...

    // Limpa a configuração da entrada padrão após cada teste
    [TestCleanup]
    public void CleanUp()
    {
        Console.SetIn(new StreamReader(Console.OpenStandardInput()));
    }
}
