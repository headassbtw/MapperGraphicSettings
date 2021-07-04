using HarmonyLib;
using IPA.Utilities;
namespace MapperGraphicSettings.Harmony_Patches
{
    [HarmonyPatch(typeof(GameCoreSceneSetup))]
    [HarmonyPatch("InstallBindings", MethodType.Normal)]
    public class GameCoreSetupHook
    {
        internal static void Prefix(GameCoreSceneSetup __instance)
        {
            //__instance.SetProperty<GameCoreSceneSetup, MainSettingsModelSO>("_mainSettingsModel", Setter.BaseGameSettingsChanger.MainSettings);
            
            //____mainSettingsModel = Setter.BaseGameSettingsChanger.MainSettings;
        }
    }
}