namespace Supplier.Desafio.Commons.MessageBus.Events;

public class DebitarLimiteClienteEvent
{
    public int IdTransacao { get; set; }
    public int IdCliente { get; set; }
    public decimal ValorADebitar { get; set; }
}