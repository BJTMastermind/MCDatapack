namespace mcdatapack.commands;

using mcdatapack.utils;

public class ListCommand {

    public static void Execute(string worldName, Utils.Editions edition) {
        string separator = Path.DirectorySeparatorChar.ToString();
        string worldFolder = $"{Utils.GetMinecraftFolder(Program.Settings.OS, edition, worldName)}{separator}{(edition == Utils.Editions.java ? "datapacks" : "behavior_packs")}";
        string packType = edition == Utils.Editions.java ? "datapacks" : "behavior packs";

        string[] packs = Directory.GetDirectories(worldFolder);
        Console.WriteLine($"All {packType} in '{worldName}':");
        foreach(string pack in packs) {
            if(pack.Contains(" ")) {
                Console.WriteLine($" '{pack.Split(separator)[pack.Split(separator).Length - 1]}'");
            } else {
                Console.WriteLine($" {pack.Split(separator)[pack.Split(separator).Length - 1]}");
            }
        }
    }
}