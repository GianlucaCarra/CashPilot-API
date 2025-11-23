namespace CashPilot.Application.Services;

public class EmailTemplateService
{
    private readonly string _templatesPath;
    
    public EmailTemplateService()
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        _templatesPath = Path.Combine(baseDirectory, "Templates", "Emails");
    }

    public async Task<string> GetVerificationEmailAsync(string name, string verificationUrl)
    {
        return await LoadTemplatesAsync("VerificationEmail", new Dictionary<string, string>()
        {
            { "Name", name },
            { "VerificationUrl", verificationUrl }
        });
    }

    private async Task<string> LoadTemplatesAsync(string templateName, Dictionary<string, string> parameters)
    {
        var filePath = Path.Combine(_templatesPath, $"{templateName}.html");

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Template file {filePath} not found");
        }
        
        var template = await File.ReadAllTextAsync(filePath);

        foreach (var param in parameters)
        {
            template = template.Replace($"{{{{{param.Key}}}}}", param.Value);
        }
        
        return template;
    }
}