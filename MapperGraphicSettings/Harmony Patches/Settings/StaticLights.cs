
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

		static GameObject replaceLabel;
		static ToggleWithCallbacks replaceToggle;

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
			var theEffect = setStatic ? EnvironmentEffectsFilterPreset.NoEffects : EnvironmentEffectsFilterPreset.AllEffects;

			toggle1.SelectCellWithLightReductionAmount(theEffect);
			toggle2.SelectCellWithLightReductionAmount(theEffect);

			instance.SetIsDirty();
		}
	}
}