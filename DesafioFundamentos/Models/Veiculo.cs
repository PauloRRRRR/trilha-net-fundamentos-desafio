namespace DesafioFundamentos.Models
{
    public class Veiculo
    {
        public string Placa { get; set; }
        public int HorasEstacionado { get; set; }

        public Veiculo(string placa)
        {
            Placa = placa;
        }
    }
}
