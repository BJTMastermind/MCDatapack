namespace mcdatapack.utils;

using Tomlyn;
using Tomlyn.Model;

public class TomlSettings {
    public string JavaMCFolder { get; set; }
    public string BedrockMCFolder { get; set; }

    public TomlSettings() {
        string toml = File.ReadAllText("./config.toml");
        TomlTable model = Toml.ToModel(toml);

        TomlTable javaEdition = (TomlTable) model["Java-Edition"];
        JavaMCFolder = (string) javaEdition["worldfolder"];

        TomlTable bedrockEdition = (TomlTable) model["Bedrock-Edition"];
        BedrockMCFolder = (string) bedrockEdition["worldfolder"];
    }

    internal static void CreateTomlConfig() {
        string[] defaultToml = {
            "[Java-Edition]",
            "worldfolder = \"\"\n",

            "[Bedrock-Edition]",
            "worldfolder = \"\""
        };
        File.WriteAllLines("config.toml", defaultToml);
    }
}