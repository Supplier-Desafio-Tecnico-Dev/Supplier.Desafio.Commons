namespace Supplier.Desafio.Commons.MessageBus.Events;

public class ResultadoTransacaoEvent
{
    public int IdTransacao { get; set; }
    public string StatusTransacao { get; set; }
}
