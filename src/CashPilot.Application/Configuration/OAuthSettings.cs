namespace CashPilot.Application.Configuration;

public class OAuthSettings
{
    public OAuthProviderSettings Google { get; set; }
    public OAuthProviderSettings GitHub { get; set; }
    public OAuthProviderSettings Microsoft { get; set; }
}