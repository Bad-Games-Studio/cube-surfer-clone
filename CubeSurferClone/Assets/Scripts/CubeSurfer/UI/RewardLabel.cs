using System;
using TMPro;
using UnityEngine;

namespace CubeSurfer.UI
{
    public class RewardLabel : MonoBehaviour
    {
        private TextMeshProUGUI _label;

        private void Awake()
        {
            _label = GetComponent<TextMeshProUGUI>();
        }

        public void SetValues(int levelReward, int multiplier)
        {
            _label.text = $"{levelReward} x {multiplier}";
        }
    }
}
