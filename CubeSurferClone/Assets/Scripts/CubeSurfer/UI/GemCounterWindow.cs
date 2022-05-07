using System;
using TMPro;
using UnityEngine;

namespace CubeSurfer.UI
{
    public class GemCounterWindow : MonoBehaviour
    {
        private TextMeshProUGUI _label;

        public int Amount
        {
            get => Convert.ToInt32(_label.text);
            set => _label.text = value.ToString();
        }
        
        private void Awake()
        {
            _label = GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}