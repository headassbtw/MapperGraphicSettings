using HarmonyLib;

namespace MapperGraphicSettings.Harmony_Patches
{
    [HarmonyPatch(typeof(LevelSelectionNavigationController))]
    [HarmonyPatch("HandleLevelCollectionNavigationControllerDidPressActionButton", MethodType.Normal)]
    internal class PlayButtonHook
    {
        internal static bool Halt = false;
        internal static bool Prefix(LevelSelectionNavigationController __instance)
        {
            Plugin.Log.Notice("AAAY");
            UI.PreSong.Controllers.DisabledWarningViewController.instance.FireModal();
            return Halt;
        }
    }
}