using System;
using UnityEngine;

namespace Json_Serializer
{   
    [Serializable]
    public class PlayerSerializer
    {
        public string name;
        public string gameClass;
        public int level;
        public int maxVit;
        public int currentVit;
        public int maxMana;
        public int currentMana;
        public int str;
        public int xpMax;
        public int currentXp;
        public int gold;
        public int[] inventory;
    }
    
}