namespace MailProcessing.Commons.Settings;

public class ImapSetting(string mailbox, string server, int port, string userName, string password)
{
    public string Server { get; init; } = server;
    public int Port { get; init; } = port;

    public string Mailbox { get; init; } = mailbox;
    public string UserName { get; init; } = userName;
    public string Password { get; init; } = password;
}