namespace mcdatapack.commands;

using mcdatapack.utils;

public class InstallCommand {
    public static void Execute(string packName, string worldName, Utils.Editions edition) {
        switch(edition) {
            case Utils.Editions.java:
                if(Program.Settings.JavaMCFolder == String.Empty) {
                    Console.WriteLine("You must first set the Java Edition .minecraft folder path before you can use this program.");
                    Console.WriteLine("Use: 'mcdatapack config set java-mcFolder <PathToMCFolder>' to set it.");
                    return;
                }
                break;
            case Utils.Editions.bedrock:
                if(Program.Settings.BedrockMCFolder == String.Empty) {
                    Console.WriteLine("You must first set the Bedrock Edition com.mojang folder path before you can use this program.");
                    Console.WriteLine("Use: 'mcdatapack config set bedrock-mcFolder <PathToMCFolder>' to set it.");
                    return;
                }
                break;
        }
    }

    // Get VanillaTweaks avalible packs json: https://vanillatweaks.net/assets/resources/json/{version}/{id}categories.json
    // version: can be one of [ 1.13, 1.14, 1.15, 1.16, 1.17, 1.18, 1.19 ]
    // id: can be one of [ rp, dp, ct ]
}