namespace mcdatapack.commands;

using mcdatapack.utils;

public class ListCommand {

    public static void Execute(string worldName, Utils.Editions edition) {
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
        string worldFolder = $"{Utils.GetMinecraftFolder(edition, worldName)}{separator}{(edition == Utils.Editions.java ? "datapacks" : "behavior_packs")}";
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