using HarmonyLib;
using UnityEngine.UI;

namespace MapperGraphicSettings.Harmony_Patches
{
    [HarmonyPatch(typeof(StandardLevelDetailView))]
    [HarmonyPatch("RefreshContent", MethodType.Normal)]
    [HarmonyAfter("com.kyle1413.BeatSaber.SongCore")]
    internal class SongSelectPatch
    {
        internal static void Postfix(StandardLevelDetailView __instance,
            ref IDifficultyBeatmap ____selectedDifficultyBeatmap)
        {
            //add shit here idk i'm moving to my desktop
        }
    }
}