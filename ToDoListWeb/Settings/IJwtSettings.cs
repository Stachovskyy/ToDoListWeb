namespace ToDoListWeb.Settings
{
    public interface IJwtSettings
    {
        int ExpirationInDays { get; set; }
        string Issuer { get; set; }
        string Secret { get; set; }
    }
}