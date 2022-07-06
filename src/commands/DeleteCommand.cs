namespace mcdatapack.commands;

using mcdatapack.utils;

public class DeleteCommand {

    public static void Execute(string projectName, string worldName, Utils.Editions edition) {
        string separator = Path.DirectorySeparatorChar.ToString();
        string worldFolder = Utils.GetMinecraftFolder(Program.Settings.OS, edition, worldName);
        string projectFolder = worldFolder + Utils.GetProjectFolder(Program.Settings.OS, edition, projectName);
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