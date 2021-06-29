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
                    var baseGame = (JObject) graphics["_baseGame"];
                    if (baseGame != null)
                    {
                        Setter.BaseGameSettingsChanger.Get();
                        foreach (var val in baseGame)
                        {
                            var v2 = val.Value;
                            Plugin.Log.Notice($"{val.Key} in base game graphics settings, with a value of {v2} and a type of {v2.GetType().ToString()}");
                            
                            if(val.Value.ToString().ToLower().Equals("true") || val.Value.ToString().ToLower().Equals("false"))
                                Setter.BaseGameSettingsChanger.BaseGameBool((bool)val.Value, val.Key);
                            else
                                Setter.BaseGameSettingsChanger.BaseGameInt((int)val.Value, val.Key);
                            
                            
                        }
                        Setter.BaseGameSettingsChanger.Set();
                    }
                        
                }
            }

        }
    }
}