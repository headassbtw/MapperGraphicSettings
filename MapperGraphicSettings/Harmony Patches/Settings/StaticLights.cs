
using HarmonyLib;
using HMUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MapperGraphicSettings.Harmony_Patches.Settings {
	[HarmonyPatch(typeof(PlayerSettingsPanelController), nameof(PlayerSettingsPanelController.SetLayout))]
	static class Staticlights {
		static PlayerSettingsPanelController instance;
		static EnvironmentEffectsFilterPresetDropdown toggle1;
		static EnvironmentEffectsFilterPresetDropdown toggle2;

		[HarmonyPriority(int.MinValue)]
		static void Postfix(
			PlayerSettingsPanelController __instance,
			EnvironmentEffectsFilterPresetDropdown ____environmentEffectsFilterDefaultPresetDropdown,
			EnvironmentEffectsFilterPresetDropdown ____environmentEffectsFilterExpertPlusPresetDropdown
		) {
			if(__instance.transform.parent.name == "PlayerSettingsViewController" || instance != null)
				return;

			instance = __instance;
			toggle1 = ____environmentEffectsFilterDefaultPresetDropdown;
			toggle2 = ____environmentEffectsFilterExpertPlusPresetDropdown;
		}

		internal static void ToggleEffectState(bool setStatic) {
			var effect = setStatic ? EnvironmentEffectsFilterPreset.NoEffects : EnvironmentEffectsFilterPreset.AllEffects;

			toggle1.SelectCellWithLightReductionAmount(effect);
			toggle2.SelectCellWithLightReductionAmount(effect);

			instance.SetIsDirty();
		}
	}
}