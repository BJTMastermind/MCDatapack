using System.CommandLine;

using mcdatapack.commands;
using mcdatapack.utils;

public class Program {
    public static TomlSettings Settings = new TomlSettings();

    public static void Main(string[] args) {
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

        /*Command installCommand = new Command(name: "install", description: "Install a avalible datapack in the spcified world for a specified version. (Defaults to Java Edition)");
        rootCommand.AddCommand(installCommand);*/

        rootCommand.Invoke(args);
    }
}