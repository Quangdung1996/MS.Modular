namespace MS.Modular.AccountManagement.Domain.Dto
{
    public class TokenInfo
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int ExpiredInMinute { get; set; }
    }
}