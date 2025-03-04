namespace Supplier.Desafio.Commons.Auth;

public interface IAuthServico
{
    string GerarToken(string email);
}