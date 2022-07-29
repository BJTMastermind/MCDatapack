namespace mcdatapack.commands;

using mcdatapack.utils;

public class ListRepoCommand {
    public static void Execute(Utils.Editions edition) {
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
    }
}