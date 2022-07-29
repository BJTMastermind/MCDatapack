namespace mcdatapack.commands;

using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

using mcdatapack.utils;

public class NewCommand {

    public static void Execute(string projectName, string projectDescription, string worldName, Utils.Editions edition) {
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
        string projectNamespace = projectName.ToLower().Replace(" ", "_");

        switch(edition) {
            case Utils.Editions.java:
                string dataFolder = $"{projectFolder}{separator}data";
                string minecraftNamespace = $"{dataFolder}{separator}minecraft{separator}tags{separator}functions{separator}";
                string customNamespace = $"{dataFolder}{separator}{projectNamespace}{separator}functions{separator}";

                if(Directory.Exists(worldFolder)) {
                    Directory.CreateDirectory(minecraftNamespace);
                    Directory.CreateDirectory(customNamespace);

                    using (FileStream fs = File.Create($"{projectFolder}{separator}pack.mcmeta")) {
                        JsonObject packmcmeta = new JsonObject();
                        JsonObject pack = new JsonObject();
                        KeyValuePair<string, JsonNode?> packDescription = new KeyValuePair<string, JsonNode?>("description", projectDescription);
                        KeyValuePair<string, JsonNode?> packFormatVersion = new KeyValuePair<string, JsonNode?>("pack_format", 10);
                        pack.Add(packDescription);
                        pack.Add(packFormatVersion);
                        packmcmeta.Add("pack", pack);
                        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
                        fs.Write(new UTF8Encoding(true).GetBytes(packmcmeta.ToJsonString(options).Replace("  ", "    ")), 0, packmcmeta.ToJsonString(options).Replace("  ", "    ").Length);
                    }

                    using (FileStream fs = File.Create(minecraftNamespace+"tick.json")) {
                        JsonObject tickjson = new JsonObject();
                        JsonArray values = new JsonArray($"{projectNamespace}:main");
                        tickjson.Add("values", values);
                        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
                        fs.Write(new UTF8Encoding(true).GetBytes(tickjson.ToJsonString(options).Replace("  ", "    ")), 0, tickjson.ToJsonString(options).Replace("  ", "    ").Length);
                    }

                    using (FileStream fs = File.Create(minecraftNamespace+"load.json")) {
                        JsonObject loadjson = new JsonObject();
                        JsonArray values = new JsonArray($"{projectNamespace}:setup");
                        loadjson.Add("values", values);
                        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
                        fs.Write(new UTF8Encoding(true).GetBytes(loadjson.ToJsonString(options).Replace("  ", "    ")), 0, loadjson.ToJsonString(options).Replace("  ", "    ").Length);
                    }

                    using (FileStream fs = File.Create(customNamespace+"main.mcfunction")) {
                        byte[] contents = new UTF8Encoding(true).GetBytes("# This file gets looped by 'tick.json'.\n");
                        fs.Write(contents, 0, contents.Length);
                    }

                    using (FileStream fs = File.Create(customNamespace+"setup.mcfunction")) {
                        byte[] contents = new UTF8Encoding(true).GetBytes("tellraw @a {\"text\":\"Hello, World!\"}\n");
                        fs.Write(contents, 0, contents.Length);
                    }

                    Console.WriteLine($"Datapack created at: {projectFolder}");
                } else {
                    Console.Error.WriteLine("World does not exist. Unable to create datapack.");
                    return;
                }
                break;
            case Utils.Editions.bedrock:
                string functionsFolder = $"{projectFolder}{separator}functions";
                string custombeNamespace = $"{functionsFolder}{separator}{projectNamespace}{separator}";

                if(Directory.Exists(worldFolder)) {
                    Directory.CreateDirectory(custombeNamespace);

                    using (FileStream fs = File.Create($"{projectFolder}{separator}manifest.json")) {
                        JsonObject manifestjson = new JsonObject();
                        KeyValuePair<string, JsonNode?> manifestVersion = new KeyValuePair<string, JsonNode?>("format_version", 2);
                        JsonObject header = new JsonObject();
                        KeyValuePair<string, JsonNode?> headerName = new KeyValuePair<string, JsonNode?>("name", projectName);
                        KeyValuePair<string, JsonNode?> headerDescription = new KeyValuePair<string, JsonNode?>("description", projectDescription);
                        KeyValuePair<string, JsonNode?> headerUUID = new KeyValuePair<string, JsonNode?>("uuid", Guid.NewGuid());
                        JsonArray headerVersion = new JsonArray(1, 0, 0);
                        JsonArray headerMinEngineVersion = new JsonArray(1, 16, 0);
                        header.Add(headerName);
                        header.Add(headerDescription);
                        header.Add(headerUUID);
                        header.Add("version", headerVersion);
                        header.Add("min_engine_version", headerMinEngineVersion);
                        JsonArray modules = new JsonArray();
                        JsonObject modulesObject = new JsonObject();
                        KeyValuePair<string, JsonNode?> modulesType = new KeyValuePair<string, JsonNode?>("type", "data");
                        KeyValuePair<string, JsonNode?> modulesUUID = new KeyValuePair<string, JsonNode?>("uuid", Guid.NewGuid());
                        JsonArray modulesVersion = new JsonArray(1, 0, 0);
                        modulesObject.Add(modulesType);
                        modulesObject.Add(modulesUUID);
                        modulesObject.Add("version", modulesVersion);
                        modules.Add(modulesObject);
                        manifestjson.Add(manifestVersion);
                        manifestjson.Add("header", header);
                        manifestjson.Add("modules", modules);
                        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
                        fs.Write(new UTF8Encoding(true).GetBytes(manifestjson.ToJsonString(options).Replace("  ", "    ")), 0, manifestjson.ToJsonString(options).Replace("  ", "    ").Length);
                    }

                    using (FileStream fs = File.Create(custombeNamespace+"main.mcfunction")) {
                        byte[] contents = new UTF8Encoding(true).GetBytes($"# Place a repeating command block in the world with the following command:\n# function {projectNamespace}/main\n# This file gets looped by the repeating command block.\n");
                        fs.Write(contents, 0, contents.Length);
                    }

                    using (FileStream fs = File.Create(custombeNamespace+"setup.mcfunction")) {
                        byte[] contents = new UTF8Encoding(true).GetBytes("tellraw @a {\"rawtext\":[{\"text\":\"Hello, World!\"}]}\n");
                        fs.Write(contents, 0, contents.Length);
                    }

                    Console.WriteLine($"Behavior Pack created at: {projectFolder}");
                    Console.WriteLine("Be sure to active the behavior pack before loading the world. Otherwise it wont load or work in game.");
                } else {
                    Console.Error.WriteLine("World does not exist. Unable to create behavior pack.");
                    return;
                }
                break;
        }
    }
}