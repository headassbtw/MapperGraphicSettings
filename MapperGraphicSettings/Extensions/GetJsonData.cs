using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using SongCore.Data;
using SongCore.Utilities;

namespace MapperGraphicSettings.Extensions
{
    public static class GetJsonData
    {
        public static JObject DiffBeatmapJson(this IDifficultyBeatmap beatmap)
        {
            if (beatmap.level is CustomPreviewBeatmapLevel customLevel)
            {
                var infoText = File.ReadAllText(customLevel.customLevelPath + "/info.dat");

                JObject info = JObject.Parse(infoText);

                var diffSets = (JArray) info["_difficultyBeatmapSets"];
                foreach (var diffSet in diffSets)
                {
                    JArray diffBeatmaps = (JArray) diffSet["_difficultyBeatmaps"];
                    foreach (JObject diffBeatmap in diffBeatmaps)
                    {
                        var diffDifficulty = Utils.ToEnum((string) diffBeatmap["_difficulty"], BeatmapDifficulty.Normal);
                        if (diffBeatmap.TryGetValue("_difficulty", out var diff))
                        {
                            if (((string) diff).Equals(beatmap.difficulty.ToString()))
                            {
                                return diffBeatmap;
                            }
                        }
                    }
                }
            }
            return null;
        }
    }
}