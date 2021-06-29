using BS_Utils.Utilities;
using UnityEngine;

namespace MapperGraphicSettings.Setter
{
    public class BaseGameSettingsChanger
    {
        private static MainSettingsModelSO MainSettings = null;

        internal static void Get()
        {
            MainSettings = SettingsFlowCoordinator.FindObjectOfType<SettingsFlowCoordinator>()
                .GetPrivateField<MainSettingsModelSO>("_mainSettingsModel");
        }
        
        internal static void BaseGameBool(bool value, string name)
        {
            if(name.ToLower().Equals("smoke"))
                MainSettings.smokeGraphicsSettings.value = value;
            else if(name.ToLower().Equals("burnmarks"))
                MainSettings.burnMarkTrailsEnabled.value = value;
            else if(name.ToLower().Equals("bloom"))
                MainSettings.mainEffectGraphicsSettings.value = value ? 1 : 0;
            else if(name.ToLower().Equals("screendistortioneffects"))
                MainSettings.screenDisplacementEffectsEnabled.value = value;
        }

        internal static void BaseGameInt(int value, string name)
        {
            if (name.ToLower().Equals("maxshockwaveparticles"))
                MainSettings.maxShockwaveParticles.value = value;
           else  if (name.ToLower().Equals("mirror"))
                MainSettings.mirrorGraphicsSettings.value = value;
        }
        
        internal static void Set()
        {
            MainSettings.Save();
            SettingsFlowCoordinator.FindObjectOfType<SettingsFlowCoordinator>().SetPrivateField("_mainSettingsModel", MainSettings);
            MainSettings.Load(true);
        }
    }
}