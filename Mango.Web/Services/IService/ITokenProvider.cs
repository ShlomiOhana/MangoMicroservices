namespace Mango.Web.Services.IService
{
    public interface ITokenProvider
    {
        void setToken(string token);
        string? GetToken();
        void ClearToken();
    }
}
