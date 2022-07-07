# MCDatapack

  MCDatapack is a C# .NET 6 command line tool to create and manage datapacks and Bedrock behavior packs.                                                                                         

## Plan To Add / To Do

- [X] 'new' Command - Creates a new datapack in a given world.
- [X] 'delete' Command - Deletes a given datapack in a world.
- [X] 'list' Command - List all datapacks in a given world.
- [ ] 'list-repo' Command - List all datapacks avalible to install.
- [ ] 'install' Command - Install a avalible datapack from the repo.
- [ ] 'config' Command - Change the default settings for the program.
- [ ] VanillaTweaks datapack support using 'install' Command.

## How to Compile

Coming Soon.

## How To Use

To create a new datapack project use the following command:<br>
``mcdatapack new <Project Name> <Project Description> <World Name> [-e|--edition <bedrock/java>]``

To delete a datapack project use the following command:<br>
``mcdatapack delete <Project Name> <World Name> [-e|--edition <bedrock/java>]``<br>
Then confirm the deletion

To list all datapacks in a world use the following command:<br>
``mcdatapack list <World Name> [-e|--edition <bedrock/java>]``

## Minimum C# Version

* C# 10
* .NET 6.0
