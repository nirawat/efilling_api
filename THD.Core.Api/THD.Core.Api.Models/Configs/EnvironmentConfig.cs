namespace THD.Core.Api.Models.Config
{
    public interface IEnvironmentConfig
    {
        string Server { get; set; }
        string BaseRoute { get; set; }
        string PathArchive { get; set; }
        string PathDocument { get; set; }
        string PathReport { get; set; }
    }

    public class EnvironmentConfig : IEnvironmentConfig
    {
        public string Server { get; set; }
        public string BaseRoute { get; set; }
        public string PathArchive { get; set; }
        public string PathDocument { get; set; }
        public string PathReport { get; set; }
    }

    public interface IEmailConfig
    {
        string Host { get; set; }
        string Port { get; set; }
        string User { get; set; }
        string Pass { get; set; }
    }

    public class EmailConfig : IEmailConfig
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }
    }
}
