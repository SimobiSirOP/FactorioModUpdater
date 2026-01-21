using System.Net.Mime;
using System.Security.Authentication;
using CommandLine;

namespace FactorioModUpdater;

public class Arguments
{
    [Option('p', "mods", Required = true, HelpText = "The path to the factorio mods folder")]
    public string ModsFolder { get; set; }
    
    [Option('u', "username", Required = true, HelpText = "The username of the factorio user")]
    public string FactorioUsername { get; set; }
    
    [Option('a', "auth-token", Required = true, HelpText = "The auth token of the factorio user")]
    public string FactorioAuthToken {get; set;}
}
public static class Master
{
    public static string MasterPath = Directory.GetCurrentDirectory();

    public static string ModFolderPath { get; private set; }

    public static string ModsFolderPath {get; private set;}

    public static HttpClient ModHttpClient { get; private set; }

    public static string FactorioUsername {get; private set;}

    public static string FactorioAuthToken {get; private set;}
    
    public static void LoadArguments(string[] args)
    {
        ParserResult<Arguments> arguments = Parser.Default.ParseArguments<Arguments>(args);
        if (arguments.Tag == ParserResultType.NotParsed)
        {
            Environment.Exit(1);
        }
        
        ModFolderPath = arguments.Value.ModsFolder;
        FactorioUsername = arguments.Value.FactorioUsername;
        FactorioAuthToken = arguments.Value.FactorioAuthToken;
    }

    public static void Load(string[] args)
    { 
        LoadArguments(args);

        var handlerOfHttpClient = new HttpClientHandler()
        {
            SslProtocols = SslProtocols.Tls13,
        };
        
        ModHttpClient = new(handlerOfHttpClient)
        {
            BaseAddress = new Uri("https://mods.factorio.com")
        };
        
        // Validation of Data
        if (string.IsNullOrEmpty(ModFolderPath))
            throw new ArgumentException("Mod folder path not specified, specify your factorio folder using --factorio-folder argument");
    }
}