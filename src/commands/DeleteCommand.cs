namespace mcdatapack.commands;

using mcdatapack.utils;

public class DeleteCommand {

    public static void Execute(string projectName, string worldName, Utils.Editions edition) {
        switch(edition) {
            case Utils.Editions.java:
                if(Program.Settings!.JavaMCFolder == String.Empty) {
                    Console.WriteLine("You must first set the Java Edition .minecraft folder path before you can use this program.");
                    Console.WriteLine("Use: 'mcdatapack config set java-mcFolder <PathToMCFolder>' to set it.");
                    return;
                }
                break;
            case Utils.Editions.bedrock:
                if(Program.Settings!.BedrockMCFolder == String.Empty) {
                    Console.WriteLine("You must first set the Bedrock Edition com.mojang folder path before you can use this program.");
                    Console.WriteLine("Use: 'mcdatapack config set bedrock-mcFolder <PathToMCFolder>' to set it.");
                    return;
                }
                break;
        }
        string separator = Path.DirectorySeparatorChar.ToString();
        string worldFolder = Utils.GetMinecraftFolder(edition, worldName);
        string projectFolder = worldFolder + Utils.GetProjectFolder(edition, projectName);
        string packType = edition == Utils.Editions.java ? "Datapack" : "Behavior Pack";

        bool shouldContinue = confirmationDialog();
        if(shouldContinue) {
            Directory.Delete(projectFolder, true);
            Console.WriteLine($"{packType} '{projectName}' was permanently deleted from: {worldFolder}");
        } else {
            Console.WriteLine("Abort.");
            return;
        }
    }

    private static bool confirmationDialog() {
        Console.Write("This action is permanent and cannot be undone,\nDo you want to continue? [Y/n] ");
        string? input = Console.ReadLine();
        if(input!.ToLower() == "y" || input!.ToLower() == "") {
            return true;
        } else {
            return false;
        }
    }
}