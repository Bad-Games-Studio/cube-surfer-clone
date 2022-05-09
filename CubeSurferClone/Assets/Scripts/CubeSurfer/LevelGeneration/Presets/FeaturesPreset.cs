using UnityEngine;

namespace CubeSurfer.LevelGeneration.Presets
{
    [CreateAssetMenu(fileName = "New Features Preset", menuName = "Level Features Preset")]
    public class FeaturesPreset : ScriptableObject
    {
        [Range(1, 5)] public int minPlayerScore;
        
        [Range(0, 20)] public int walls;
        [Range(0, 10)] public int turns;
        [Range(0, 10)] public int lavaLakes;
    }
}
