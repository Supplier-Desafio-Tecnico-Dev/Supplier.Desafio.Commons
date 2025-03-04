namespace Supplier.Desafio.Commons.Auth
{
    public class JwtConfig
    {
        public string Segredo { get; set; } = string.Empty;
        public string Emissor { get; set; } = string.Empty;
        public string ValidoEm { get; set; } = string.Empty;
        public double ExpiracaoHoras { get; set; }
    }
}
