using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Crouch
{
    [BepInPlugin(Guid, Name, Version), BepInDependency("Terrain.MuckSettings")]
    public class Main : BaseUnityPlugin
    {
        public const string
            Name = "Crouch",
            Author = "Terrain",
            Guid = Author + "." + Name,
            Version = "1.0.0.0";

        internal readonly ManualLogSource log;
        internal readonly Harmony harmony;
        internal readonly Assembly assembly;
        public readonly string modFolder;
        public static string savefile;

        Main()
        {
            log = Logger;
            harmony = new Harmony(Guid);
            assembly = Assembly.GetExecutingAssembly();
            modFolder = Path.GetDirectoryName(assembly.Location);
            savefile = Path.Combine(modFolder, "binds");

            LoadBinds();

            harmony.PatchAll(assembly);
        }

        public static KeyCode bind = KeyCode.LeftControl;

        public static void LoadBinds()
        {
            if (!File.Exists(savefile)) return;
            using (var file = File.Open(savefile, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(file))
            {
                bind = (KeyCode)reader.ReadInt32();
            }
        }

        public static void SaveBind(KeyCode key)
        {
            bind = key;
            using (var file = File.Open(savefile, FileMode.OpenOrCreate, FileAccess.Write))
            {
                file.SetLength(0);
                using (var writer = new BinaryWriter(file))
                {
                    writer.Write((int)bind);
                }
            }
        }
    }
}