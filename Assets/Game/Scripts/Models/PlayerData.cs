using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Models
{
    public static class PlayerData
    {
        public static int Level
        {
            get => PlayerPrefs.GetInt("Level", 1);
            set => PlayerPrefs.SetInt("Level", value);
        }
        public static int Coin
        {
            get => PlayerPrefs.GetInt("Coin", 0);
            set => PlayerPrefs.SetInt("Coin", value);
        }
    }
}