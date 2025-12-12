using System.Security.Claims;

namespace Financ.UI.Api.Extensao
{
    public static class AuxiliarAutenticacao
    {
        public static string RetornaIdUsuario(this ClaimsPrincipal usuario)
        {
            return usuario.FindFirst(ClaimTypes.NameIdentifier)!.Value.ToLower();
        }
    }
}
