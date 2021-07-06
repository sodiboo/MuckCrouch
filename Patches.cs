using HarmonyLib;
using UnityEngine;

namespace Crouch
{
    [HarmonyPatch]
    class CrouchPatches
    {
        [HarmonyPatch(typeof(PlayerInput), nameof(PlayerInput.MyInput)), HarmonyPrefix]
        static void MyInput(PlayerInput __instance)
        {
            __instance.crouching = Input.GetKey(Main.crouch.Value);
        }

        [HarmonyPatch(typeof(MuckSettings.Settings), nameof(MuckSettings.Settings.Controls)), HarmonyPostfix]
        static void Controls(MuckSettings.Settings.Page page) {
            page.AddControlSetting("Crouch", Main.crouch);
        }
    }
}