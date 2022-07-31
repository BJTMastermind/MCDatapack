namespace mcdatapack.commands;

using System.Text.Json.Nodes;

using mcdatapack.utils;

public class ListRepoCommand {
    public static void Execute(Utils.Editions edition) {
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
        Task<string> json = Utils.getPacks(edition);
        JsonArray packList = (JsonArray) JsonNode.Parse(json.Result);

        foreach(JsonNode item in packList) {
            string name = item["name"].ToString();
            Console.WriteLine(name.Split(new char[] {'[', ']'})[1]);
        }
    }
}