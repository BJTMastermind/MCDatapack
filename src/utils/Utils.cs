namespace mcdatapack.utils;

public class Utils {
    public enum Editions {
        java,
        bedrock
    }

    public static string GetMinecraftFolder(Editions edition, string worldName) {
        string username = Environment.UserName;
        switch(edition) {
            case Editions.java:
                return String.Format($"{Program.Settings.JavaMCFolder}", username, worldName);
            case Editions.bedrock:
                return findBedrockWorld(String.Format($"{Program.Settings.BedrockMCFolder}", username, worldName).Replace(worldName, ""), worldName);
            default:
                goto case Editions.java;
        }
    }

    public static string GetProjectFolder(Editions edition, string datapackName) {
        string separator = Path.DirectorySeparatorChar.ToString();
        switch(edition) {
            case Editions.java:
                return String.Format($"{separator}datapacks{separator}{datapackName}{separator}");
            case Editions.bedrock:
                return String.Format($"{separator}behavior_packs{separator}{datapackName}{separator}");
            default:
                return String.Format($"{separator}datapacks{separator}{datapackName}{separator}");
        }
    }

    private static string findBedrockWorld(string worldFolder, string worldName) {
        string separator = Path.DirectorySeparatorChar.ToString();
        string[] directories = Directory.GetDirectories(worldFolder);
        foreach(string directory in directories) {
            if(File.Exists($"{directory}{separator}levelname.txt")) {
                string file = $"{directory}{separator}levelname.txt";
                string contents = File.ReadAllText(file);
                if(contents == worldName) {
                    return worldFolder + directory.Split(separator)[directory.Split(separator).Length - 1];
                }
            }
        }
        return String.Empty;
    }

    internal static async Task<string> getPacks(Utils.Editions edition) {
        // Java Datapacks Link: https://api.github.com/repos/BJTMastermind/MCDatapack/contents/packages/java
        // Bedrock Behavior Packs Link: https://api.github.com/repos/BJTMastermind/MCDatapack/contents/packages/bedrock

        string url = $"https://api.github.com/repos/BJTMastermind/MCDatapack/contents/packages/{edition}";

        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
        HttpResponseMessage response = await client.GetAsync(url);

        return response.Content.ReadAsStringAsync().Result;
    }
}