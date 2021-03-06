using UnityEngine;

namespace CubeSurfer.LevelGeneration.Presets
{
    [CreateAssetMenu(fileName = "New Level Objects Preset", menuName = "Level Objects Preset")]
    public class ObjectsPreset : ScriptableObject
    {
        public GameObject startPlatform;
        public GameObject finishPlatform;
        
        public GameObject standardPlatform;
        
        public GameObject turnLeftPlatform;
        public GameObject turnRightPlatform;

        public GameObject[] wallsPlatforms;

        public GameObject[] bonusPlatforms;

        public GameObject[] lavaPlatforms;
    }
}
