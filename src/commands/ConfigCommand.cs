namespace mcdatapack.commands;

using Tomlyn;
using Tomlyn.Model;

public class ConfigCommand {
    public static void SetConfigOption(string configOption, DirectoryInfo value) {
        TomlTable settings = Toml.ToModel(File.ReadAllText("config.toml"));
        switch(configOption) {
            case "java-worldfolder":
                TomlTable java = (TomlTable) settings["Java-Edition"];
                java["worldfolder"] = $"{value.FullName.Replace(Environment.UserName, "{0}")}{{1}}";
                break;
            case "bedrock-worldfolder":
                TomlTable bedrock = (TomlTable) settings["Bedrock-Edition"];
                bedrock["worldfolder"] = $"{value.FullName.Replace(Environment.UserName, "{0}")}{{1}}";;
                break;
        }
        File.WriteAllText("config.toml", Toml.FromModel(settings));
    }

    public static void GetConfigOption(string configOption) {
        switch(configOption) {
            case "java-worldfolder":
                Console.WriteLine(String.Format($"{Program.Settings!.JavaMCFolder}", Environment.UserName, ""));
                break;
            case "bedrock-worldfolder":
                Console.WriteLine(String.Format($"{Program.Settings!.BedrockMCFolder}", Environment.UserName, ""));
                break;
        }
    }
}