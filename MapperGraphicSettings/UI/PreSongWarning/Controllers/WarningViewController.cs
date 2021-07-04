using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SongCore;
using System.Reflection;
using BeatSaberMarkupLanguage.Parser;
using HarmonyLib;
using HMUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static BeatSaberMarkupLanguage.Components.CustomListTableData;

namespace MapperGraphicSettings.UI.PreSong.Controllers
{
    public class DisabledWarningViewController : NotifiableSingleton<DisabledWarningViewController>
    {
        private StandardLevelDetailViewController standardLevel;

        //Currently selected song data
        public CustomPreviewBeatmapLevel level;
        public SongCore.Data.ExtraSongData songData;
        public SongCore.Data.ExtraSongData.DifficultyData diffData;

        private bool _buttonGlow = true;
        [UIValue("button-glow")]
        public bool ButtonGlow
        {
            get => _buttonGlow;
            set
            {
                _buttonGlow = value;
                NotifyPropertyChanged();
            }
        }

        [UIAction("#post-parse")]
        internal void PostParse()
        {
            
        }
        
        [UIParams]
        BSMLParserParams parserParams;
        
        private bool _buttonInteractable = true;
        [UIValue("button-interactable")]
        public bool ButtonInteractable
        {
            get => _buttonInteractable;
            set
            {
                _buttonInteractable = value;
                NotifyPropertyChanged();
            }
        }

        [UIComponent("info-button")]
        private Transform infoButtonTransform;

        internal void Setup()
        {
            
            
            ShowButton();
            standardLevel = Resources.FindObjectsOfTypeAll<StandardLevelDetailViewController>().First();
            BSMLParser.instance.Parse(BeatSaberMarkupLanguage.Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), "MapperGraphicSettings.UI.PreSongWarning.Views.Warning.bsml"), standardLevel.transform.Find("LevelDetail").gameObject, this);
            infoButtonTransform.localScale *= 0.7f;//no scale property in bsml as of now so manually scaling it
            (standardLevel.transform.Find("LevelDetail").Find("FavoriteToggle")?.transform as RectTransform).anchoredPosition = new Vector2(3, -2);
        }

        
        internal void FireModal(List<string> suggestions = null)
        {
            parserParams.EmitEvent("open-modal");
        }
        [UIAction("continue")]
        internal void Continue()
        {
            Harmony_Patches.PlayButtonHook.Halt = true;
            var controller = FindObjectOfType<LevelSelectionNavigationController>();
            controller.HandleLevelCollectionNavigationControllerDidPressActionButton(FindObjectOfType<LevelCollectionNavigationController>());
            Harmony_Patches.PlayButtonHook.Halt = false;
        }
        
        internal void ShowButton()
        {
            ButtonInteractable = true;
            ButtonGlow = true;
            Harmony_Patches.PlayButtonHook.Halt = false;
        }

        [UIAction("select")]
        public void DeviceSelect(TableView _, int row)
        {
        }

        internal void HideButton()
        {
            ButtonInteractable = false;
            ButtonGlow = false;
            Harmony_Patches.PlayButtonHook.Halt = true;
        }
        

        [UIAction("button-click")]
        internal void ShowRequirements()
        {
        }
    }
}
