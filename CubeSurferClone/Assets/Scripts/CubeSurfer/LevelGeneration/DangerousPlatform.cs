using UnityEngine;

namespace CubeSurfer.LevelGeneration
{
    public class DangerousPlatform : MonoBehaviour
    {
        [SerializeField] private int minScoreToLose;
        public int MinScoreToLose => minScoreToLose;
    }
}
