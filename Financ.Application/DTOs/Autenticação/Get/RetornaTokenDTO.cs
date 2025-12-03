namespace Financ.Application.DTOs.Autenticação.Get
{
    public class RetornaTokenDTO
    {
        public string Token { get; set; }
        public DateTime Expiracao { get; set; }
    }
}
