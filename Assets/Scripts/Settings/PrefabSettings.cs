using System;
using UnityEngine;

namespace TestDemo
{
    [Serializable]
    public class PrefabSettings
    {
        public GOSettings Platform;
        public GOSettings Crystall;
        public GOSettings FX;
    }

    [Serializable]
    public class GOSettings
    {
        public int InitialCount;
        public GameObject GO;
        public string Group;
    }
}
