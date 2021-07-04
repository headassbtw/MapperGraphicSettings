using HarmonyLib;
using HMUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BS_Utils.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace MapperGraphicSettings.Harmony_Patches.Settings {
    [HarmonyPatch(typeof(PlayerSettingsPanelController), nameof(PlayerSettingsPanelController.SetLayout))]
    static class Debris {
        static PlayerSettingsPanelController instance;
        static Toggle toggle;


        [HarmonyPriority(int.MinValue)]
        static void Postfix(
            PlayerSettingsPanelController __instance,
            Toggle ____reduceDebrisToggle
        ) {
            if(__instance.transform.parent.name == "PlayerSettingsViewController" || instance != null)
                return;

            instance = __instance;
            toggle = ____reduceDebrisToggle;
        }

        internal static void ToggleDebris(bool state)
        {
            toggle.isOn = state;
            instance.SetIsDirty();
        }
    }
}