using Assets.Game.Scripts.Controllers;
using Cinemachine;
using Game.Scripts.Behaviours;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Controllers
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;

        [SerializeField] private CinemachineVirtualCamera _FinishCam;
        public Camera Camera => _mainCamera;

        private void Awake()
        {
            FinishLineBehaviour.FinishLinePassed += OnFinishLinePassed;
        }

        private void OnDestroy()
        {
            FinishLineBehaviour.FinishLinePassed -= OnFinishLinePassed;
        }

        private void OnFinishLinePassed()
        {
            _FinishCam.gameObject.SetActive(true);

            GameController.GameOver += OnGameOver;
        }

        private void OnGameOver()
        {
            GameController.GameOver -= OnGameOver;
            _FinishCam.gameObject.SetActive(false);
        }

        public Vector2 GetMouseWorldPosition()
        {
            return Camera.ScreenToWorldPoint(Input.mousePosition);
        }

        public Ray GetScreenPointToRay()
        {
            return Camera.ScreenPointToRay(Input.mousePosition);
        }
    }
}