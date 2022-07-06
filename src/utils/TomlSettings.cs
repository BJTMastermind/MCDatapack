namespace mcdatapack.utils;

using Tomlyn;
using Tomlyn.Model;

public class TomlSettings {
    public string OS { get; set; }

    public string JavaWindowsWorldFolder { get; set; }
    public string JavaMacWorldFolder { get; set; }
    public string JavaLinuxWorldFolder { get; set; }

    public string JavaWindowsProjectFolder { get; set; }
    public string JavaOtherProjectFolder { get; set; }

    public string BedrockWindowsWorldFolder { get; set; }
    public string BedrockMacWorldFolder { get; set; }
    public string BedrockLinuxWorldFolder { get; set; }

    public string BedrockWindowsProjectFolder { get; set; }
    public string BedrockOtherProjectFolder { get; set; }

    public TomlSettings() {
        string toml = File.ReadAllText("./config.toml");
        TomlTable model = Toml.ToModel(toml);

        OS = (string) model["OS"];

        TomlTable javaEdition = (TomlTable) model["Java-Edition"];
        JavaWindowsWorldFolder = (string) javaEdition["windows-worldfolder"];
        JavaMacWorldFolder = (string) javaEdition["mac-worldfolder"];
        JavaLinuxWorldFolder = (string) javaEdition["linux-worldfolder"];

        JavaWindowsProjectFolder = (string) javaEdition["windows-projectroot"];
        JavaOtherProjectFolder = (string) javaEdition["other-projectroot"];

        TomlTable bedrockEdition = (TomlTable) model["Bedrock-Edition"];
        BedrockWindowsWorldFolder = (string) bedrockEdition["windows-worldfolder"];
        BedrockMacWorldFolder = (string) bedrockEdition["mac-worldfolder"];
        BedrockLinuxWorldFolder = (string) bedrockEdition["linux-worldfolder"];

        BedrockWindowsProjectFolder = (string) bedrockEdition["windows-projectroot"];
        BedrockOtherProjectFolder = (string) bedrockEdition["other-projectroot"];
    }
}