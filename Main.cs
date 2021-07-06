using BepInEx;
using BepInEx.Configuration;
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
        
        public static ConfigFile config = new ConfigFile(Path.Combine(Paths.ConfigPath, "crouch.cfg"), true);
        public static ConfigEntry<KeyCode> crouch = config.Bind<KeyCode>("Crouch", "crouch", KeyCode.LeftControl, "The button that makes you crouch.");

        Main()
        {
            log = Logger;
            harmony = new Harmony(Guid);
            assembly = Assembly.GetExecutingAssembly();
            modFolder = Path.GetDirectoryName(assembly.Location);

            // If anyone's using this as a base for their config, make sure to also copy this line!
            config.SaveOnConfigSet = true;
            harmony.PatchAll(assembly);
        }
    }
}