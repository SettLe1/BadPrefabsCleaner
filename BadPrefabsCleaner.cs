using System.Collections.Generic;
using Oxide.Core;

namespace Oxide.Plugins
{
   [Info("BadPrefabsCleaner", "SettLe", "0.1.1")]
   class BadPrefabsCleaner : RustPlugin
   {
      private void Loaded()
      {
         cmd.AddConsoleCommand("clearprefabs", this, nameof(cmdConsole));
      }

      private void cmdConsole(ConsoleSystem.Arg arg)
      {
         if (!arg.IsAdmin) return;

         var allowedPrefabs = Interface.Oxide.DataFileSystem.ReadObject<Dictionary<uint, string>>("AllowedPrefabs");
         if (allowedPrefabs.IsNullOrEmpty())
         {
            arg.ReplyWith("Error on loading list of allowed prefabs. Get actual list named 'AllowedPrefabs' (github.com/SettLe1/BadPrefabsCleaner) and put into oxide/data folder.");
            return;
         }

         Dictionary<uint, string> removedPrefabs = null;
         foreach (var pd in World.Serialization.world.prefabs.ToArray())
         {
            if (!allowedPrefabs.ContainsKey(pd.id))
            {
               if (removedPrefabs is null) 
                  removedPrefabs = new Dictionary<uint, string>{{pd.id, StringPool.Get(pd.id)}};
               else removedPrefabs.TryAdd(pd.id, StringPool.Get(pd.id));
                  
               World.Serialization.world.prefabs.RemoveAll(x => x.id == pd.id);
            }
         }

         if (removedPrefabs.IsNullOrEmpty())
         {
            arg.ReplyWith("Bad prefabs not found on current map.");
            return;
         }
         
         var logFile = string.Concat(World.Name, ".RemovedPrefabs");
         var newmap = string.Concat(World.Name, ".cleared.map");
         World.Serialization.Save(newmap);
         Interface.Oxide.DataFileSystem.WriteObject(logFile, removedPrefabs);
         arg.ReplyWith(string.Concat("Bad prefabs removed from current map (list saved into oxide/data/", logFile, ".json).\nNew map saved into root dir (", newmap, ")"));
      }
      
   }
}