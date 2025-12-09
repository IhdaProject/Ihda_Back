namespace Entity.Models.Configurations;

public class MinIoConfiguration
{
    public string Endpoint { get; set; }
    public string AccessKey { get; set; }
    public string SecretKey { get; set; }
    public bool UseSSL { get; set; }
}