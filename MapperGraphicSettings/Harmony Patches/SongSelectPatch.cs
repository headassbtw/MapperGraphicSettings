using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using IPA.Config.Data;
using IPA.Utilities;
using Newtonsoft.Json.Linq;
using SongCore.Data;
using SongCore.Utilities;
using UnityEngine.UI;
using MapperGraphicSettings.Extensions;

namespace MapperGraphicSettings.Harmony_Patches
{
    [HarmonyPatch(typeof(StandardLevelDetailView))]
    [HarmonyPatch("RefreshContent", MethodType.Normal)]
    internal class SongSelectPatch
    {
        internal static void Postfix(StandardLevelDetailView __instance,
            ref IDifficultyBeatmap ____selectedDifficultyBeatmap)
        {
            List<string> bools = new List<string>();
            JObject diffBeatmap = (JObject) ____selectedDifficultyBeatmap.DiffBeatmapJson();
            if (diffBeatmap.TryGetValue("_customData", out var customData))
            {
                Plugin.Log.Notice("Map has custom data");
                JObject beatmapData = (JObject)customData;

                if (beatmapData.TryGetValue("_graphics", out var graphics))
                {
                    //UI.PreSong.Controllers.DisabledWarningViewController.instance.ShowButton();
                    Plugin.Log.Notice("Map has graphics suggestions");
                    var baseGame = (JArray) graphics["_baseGame"];
                    var chroma = (JArray) graphics["_chroma"];
                    foreach (var val in baseGame)
                    {
                        Plugin.Log.Notice($"Value {val.ToString()} in base game graphics settings");
                    }
                    foreach(var val in chroma)
                        Plugin.Log.Notice($"Value {val.ToString()} in Chroma graphics settings");
                }
            }

        }
    }
}