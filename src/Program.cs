using System.CommandLine;

using mcdatapack.commands;
using mcdatapack.utils;

public class Program {
    public static TomlSettings? Settings;

    public static void Main(string[] args) {
        if(!File.Exists("config.toml")) {
            TomlSettings.CreateTomlConfig();
        }

        Settings = new TomlSettings();

        RootCommand rootCommand = new RootCommand("A package manager for Minecraft datapacks.");

        Option<Utils.Editions> edition = new Option<Utils.Editions>(name: "--edition", description: "The edition of minecraft version (Defaults to Java)");
        edition.AddAlias("-e");

        Command newCommand = new Command(name: "new", description: "Creates a new datapack in the spcified world for the specified version. (Defaults to Java Edition)");
        Argument<string> projectName = new Argument<string>(name: "Project Name", description: "The name of the datapack.");
        newCommand.AddArgument(projectName);

        Argument<string> projectDescription = new Argument<string>(name: "Description", description: "The description of the datapack.");
        newCommand.AddArgument(projectDescription);

        Argument<string> worldFolder = new Argument<string>(name: "World Name", description: "The world the create the datapack project in.");

        newCommand.AddArgument(worldFolder);
        newCommand.AddOption(edition);

        newCommand.SetHandler((projectNameValue, projectDescriptionValue, worldFolderValue, editionValue) => {
            NewCommand.Execute(projectNameValue, projectDescriptionValue, worldFolderValue, editionValue);
        }, projectName, projectDescription, worldFolder, edition);

        rootCommand.AddCommand(newCommand);

        Command deleteCommand =  new Command(name: "delete", description: "Deletes the entire project from the world.");
        Argument<string> deleteProjectName = new Argument<string>(name: "Project Name", description: "The name of the datapack to delete.");
        Argument<string> deleteWorldFolder = new Argument<string>(name: "World Name", description: "The world the delete the datapack project from.");

        deleteCommand.AddArgument(deleteProjectName);
        deleteCommand.AddArgument(deleteWorldFolder);
        deleteCommand.AddOption(edition);

        deleteCommand.SetHandler((deleteProjectNameValue, deleteWorldFolderValue, editionValue) => {
            DeleteCommand.Execute(deleteProjectNameValue, deleteWorldFolderValue, editionValue);
        }, deleteProjectName, deleteWorldFolder, edition);

        rootCommand.AddCommand(deleteCommand);

        Command listCommand = new Command(name: "list", description: "List all datapacks in the given world for a specified version. (Defaults to Java Edition)");
        Argument<string> listWorldFolder = new Argument<string>(name: "World Name", description: "The world to list all datapacks from.");
        listCommand.AddArgument(listWorldFolder);
        listCommand.AddOption(edition);

        listCommand.SetHandler((listWorldFolderValue, editionValue) => {
            ListCommand.Execute(listWorldFolderValue, editionValue);
        }, listWorldFolder, edition);

        rootCommand.AddCommand(listCommand);

        Command listrepoCommand = new Command(name: "list-repo", description: "List all avalible datapacks to install for a specified version. (Defaults to Java Edition)");
        listrepoCommand.AddOption(edition);

        listrepoCommand.SetHandler((editionValue) => {
            ListRepoCommand.Execute(editionValue);
        }, edition);

        rootCommand.AddCommand(listrepoCommand);

        Command installCommand = new Command(name: "install", description: "Install a avalible datapack in the spcified world for a specified version. (Defaults to Java Edition)");
        Argument<string> packName = new Argument<string>(name: "Datapack", description: "The datapack name to install.");
        Argument<string> installWorldFolder = new Argument<string>(name: "World Name", description: "The world to install the datapack too.");
        installCommand.AddArgument(packName);
        installCommand.AddArgument(installWorldFolder);
        installCommand.AddOption(edition);

        installCommand.SetHandler((packNameValue, installWorldFolderValue, editionValue) => {
            InstallCommand.Execute(packNameValue, installWorldFolderValue, editionValue);
        }, packName, installWorldFolder, edition);

        rootCommand.AddCommand(installCommand);

        Command configCommand = new Command(name: "config", description: "Get/Change the settings of the program.");
        Command configSet = new Command(name: "set", description: "Change the default settings of the program.");
        Command configGet = new Command(name: "get", description: "Get the current value of a setting for the program.");

        Command setJavaMCFolder = new Command(name: "java-mcFolder", description: "Set the location of the Java edition .minecraft folder.");
        Command getJavaMCFolder = new Command(name: "java-mcFolder", description: "Get the location of the Java edition .minecraft folder.");
        Argument<DirectoryInfo> javamcPath = new Argument<DirectoryInfo>();
        setJavaMCFolder.AddArgument(javamcPath);
        configSet.AddCommand(setJavaMCFolder);
        configGet.AddCommand(getJavaMCFolder);

        Command setBedrockMCFolder = new Command(name: "bedrock-mcFolder", description: "Set the location of the Bedrock edition com.mojang folder.");
        Command getBedrockMCFolder = new Command(name: "bedrock-mcFolder", description: "Get the location of the Bedrock edition com.mojang folder.");
        Argument<DirectoryInfo> bedrockmcPath = new Argument<DirectoryInfo>();
        setBedrockMCFolder.AddArgument(bedrockmcPath);
        configSet.AddCommand(setBedrockMCFolder);
        configGet.AddCommand(getBedrockMCFolder);

        setJavaMCFolder.SetHandler((javamcPathValue) => {
            ConfigCommand.SetConfigOption("java-worldfolder", javamcPathValue);
        }, javamcPath);

        setBedrockMCFolder.SetHandler((bedrockmcPathValue) => {
            ConfigCommand.SetConfigOption("bedrock-worldfolder", bedrockmcPathValue);
        }, bedrockmcPath);

        getJavaMCFolder.SetHandler(() => {
            ConfigCommand.GetConfigOption("java-worldfolder");
        });

        getBedrockMCFolder.SetHandler(() => {
            ConfigCommand.GetConfigOption("bedrock-worldfolder");
        });

        configCommand.AddCommand(configSet);
        configCommand.AddCommand(configGet);

        rootCommand.AddCommand(configCommand);

        rootCommand.Invoke(args);
    }
}