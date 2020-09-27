using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Behaviours
{
    public class LevelBehaviour : MonoBehaviour
    {
        [SerializeField] private List<RoadBehaviour> _roads;

        public void Initialize()
        {
            foreach (var road in _roads)
            {
                road.UpdatePath();
            }
        }
    }
}