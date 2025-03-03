namespace Supplier.Desafio.Commons.Notificacoes;

public interface INotificador
{
    bool TemNotificacao();
    IEnumerable<Notificacao> ObterNotificacoes();
    void Handle(Notificacao notificacao);
}
