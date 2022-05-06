using UnityEngine;

namespace CubeSurfer.CollisionTag
{
    public class BonusZone : MonoBehaviour
    {
        [SerializeField] private int scoreMultiplier;
        public int ScoreMultiplier => scoreMultiplier;
    }
}
