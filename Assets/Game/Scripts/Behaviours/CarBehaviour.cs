using Game.Scripts.View;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Behaviours
{
    public class CarBehaviour : MonoBehaviour
    {
        [SerializeField] private CarModel _carModel;
        public CarModel CarModel => _carModel;

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            _carModel.Initialize();
            UiDraw.LineDrew += OnLineDrew;
        }

        public void Dispose()
        {
            UiDraw.LineDrew -= OnLineDrew;
        }

        private void OnLineDrew(List<Vector2> controlPoints)
        {
            _carModel.CreateMesh(controlPoints);
        }

    }
}