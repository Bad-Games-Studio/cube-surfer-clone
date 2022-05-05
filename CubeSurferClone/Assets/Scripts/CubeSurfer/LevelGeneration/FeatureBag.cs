using System;
using UnityEngine;

namespace CubeSurfer.LevelGeneration
{
    public class FeatureBag
    {
        public enum ObjectType
        {
            Wall, LavaLake, Turn
        }

        public ObjectType Type { get; }
        
        private int _amount;
        public int Amount
        {
            get => _amount;
            set => _amount = Mathf.Clamp(value, 0, int.MaxValue);
        }

        public bool Empty => _amount == 0;

        public FeatureBag(ObjectType type, int amount)
        {
            Type = type;
            Amount = amount;
        }

        public FeatureBag TakeOne()
        {
            if (Empty)
            {
                throw new Exception("No more features of that type can be taken from a bag.");
            }
            
            --Amount;
            return new FeatureBag(Type, 1);
        }
    }
}