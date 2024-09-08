using System;
using UnityEngine;

namespace ScriptableObjects
{
    [Serializable]
    [CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData")]
    public class GameData : ScriptableObject
    {
        [field: SerializeField]
        public int Lives { get; private set; }
        
        [field: SerializeField]
        public int Score { get; private set; }
        
        [field: SerializeField]
        public string LevelName { get; private set; }
    }
}