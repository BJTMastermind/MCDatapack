namespace mcdatapack.utils;

public class Utils {
    public enum Editions {
        java,
        bedrock
    }

    public static string GetMinecraftFolder(string os, Editions edition, string worldName) {
        string username = Environment.UserName;
        switch(edition) {
            case Editions.java:
                switch(os) {
                    case "Windows":
                        return String.Format($"{Program.Settings.JavaWindowsWorldFolder}", username, worldName);
                    case "MacOs":
                        return String.Format($"{Program.Settings.JavaMacWorldFolder}", username, worldName);
                    case "Linux":
                        return String.Format($"{Program.Settings.JavaLinuxWorldFolder}", username, worldName);
                    default:
                        return String.Empty;
                }
            case Editions.bedrock:
                switch(os) {
                    case "Windows":
                        return findBedrockWorld(String.Format($"{Program.Settings.BedrockWindowsWorldFolder}", username, worldName).Replace(worldName, ""), worldName);
                    case "MacOs":
                        return findBedrockWorld(String.Format($"{Program.Settings.BedrockMacWorldFolder}", username, worldName).Replace(worldName, ""), worldName);
                    case "Linux":
                        return findBedrockWorld(String.Format($"{Program.Settings.BedrockLinuxWorldFolder}", username, worldName).Replace(worldName, ""), worldName);
                    default:
                        return String.Empty;
                }
            default:
                goto case Editions.java;
        }
    }

    public static string GetProjectFolder(string os, Editions edition, string datapackName) {
        switch(edition) {
            case Editions.java:
                switch(os) {
                    case "Windows":
                        return String.Format($"{Program.Settings.JavaWindowsProjectFolder}", datapackName);
                    case "MacOs":
                        return String.Format($"{Program.Settings.JavaOtherProjectFolder}", datapackName);
                    case "Linux":
                        return String.Format($"{Program.Settings.JavaOtherProjectFolder}", datapackName);
                    default:
                        return String.Empty;
                }
            case Editions.bedrock:
                switch(os) {
                    case "Windows":
                        return String.Format($"{Program.Settings.BedrockWindowsProjectFolder}", datapackName);
                    case "MacOs":
                        return String.Format($"{Program.Settings.BedrockOtherProjectFolder}", datapackName);
                    case "Linux":
                        return String.Format($"{Program.Settings.BedrockOtherProjectFolder}", datapackName);
                    default:
                        return String.Empty;
                }
            default:
                goto case Editions.java;
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
}