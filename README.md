# BadPrefabsCleaner
This plugin for the Rust (game) server with Oxide mod allows you to remove bad prefabs from your custom map that cause critical server errors.

## Getting started
1. Make sure your server is updated and has the latest version of Oxide installed
  *server installing guide: https://wiki.facepunch.com/rust/Creating-a-server  
  hosting custom map guide: https://wiki.facepunch.com/rust/Hosting_a_custom_map
  download Oxide here: umod.org/games/rust*
2. Put the **AllowedPrefabs.json** into **oxide/data** folder (if the folder does not exist you can create it)
3. Put the **BadPrefabsCleaner.cs** into **oxide/plugins** folder
4. Run your server and use command **clearprefabs** in console after loading map
5. If bad prefabs were removed from your map, they will be listed in the file **oxide/data/mapname.RemovedPrefabs.json**. A new map without bad prefabs will be saved to the server root folder.

![{7D1A7251-FC1F-47FA-8FF1-216CC3EB9259}](https://github.com/user-attachments/assets/473f7979-d604-4a62-bdec-9ce61eb9ab10)
