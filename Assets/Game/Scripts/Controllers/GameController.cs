using Game.Scripts.Behaviours;
using Game.Scripts.View;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mek.Controllers;
using Game.Scripts.Models;

namespace Assets.Game.Scripts.Controllers
{
    public class GameController : MonoBehaviour
    {
        public static event Action GameStarted;
        public static event Action GameOver;

        public LevelBehaviour CurrentLevel;

        [SerializeField] private CarBehaviour _car;
        [SerializeField] private List<LevelBehaviour> _levels;

        public static float FinishTimerAfterFinishLinePassed = 5f;
        private string FinishRoutineKey => $"FinishRoutine{GetInstanceID()}";

        private void Awake()
        {
            PrepareLevel();
        }

        private void PrepareLevel()
        {
            CurrentLevel = Instantiate(_levels[(PlayerData.Level - 1) % _levels.Count]);
            CurrentLevel.Initialize();
            _car.Initialize();

            GameStarted?.Invoke();

            ViewController.Instance.InGameView.Open(new InGameViewParameters());

            FinishLineBehaviour.FinishLinePassed += OnFinishLinePassed;
        }

        private void DisposeLevel()
        {
            CoroutineController.DoAfterGivenTime(1f, () =>
            {
                Destroy(CurrentLevel.gameObject);
                _car.Dispose();
                ViewController.Instance.GameOverView.Close();
                PrepareLevel();
            });
        }

        private void OnGameOver()
        {
            ViewController.Instance.InGameView.Close();
            ViewController.Instance.GameOverView.Open(new GameOverViewParameters(PlayerData.Level * 50, OnGameOverViewCompleted));

            PlayerData.Level++;

            GameOver?.Invoke();
        }

        private void NextLevel()
        {
            DisposeLevel();
        }

        #region Helpers

        private void OnFinishLinePassed()
        {
            FinishLineBehaviour.FinishLinePassed -= OnFinishLinePassed;

            ChestBehaviour.ChestClaimed += OnChestClaimed;

            CoroutineController.StartCoroutine(FinishRoutineKey, FinishRoutine());
        }

        private void OnChestClaimed()
        {
            ChestBehaviour.ChestClaimed -= OnChestClaimed;

            if (CoroutineController.IsCoroutineRunning(FinishRoutineKey))
            {
                CoroutineController.StopThisCoroutine(FinishRoutineKey);
            }

            CoroutineController.DoAfterGivenTime(2f, OnGameOver);

            Debug.Log("ChestClaimed");
        }

        private IEnumerator FinishRoutine()
        {
            var timer = FinishTimerAfterFinishLinePassed;
            while (timer > 0)
            {
                timer -= Time.fixedDeltaTime;
                yield return null;
            }

            OnGameOver();
        }

        private void OnGameOverViewCompleted()
        {
            NextLevel();
        }

        #endregion

        #region Singleton

        private static GameController _instance;

        public static GameController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameController>();

                    if (_instance == null)
                    {
                        Debug.LogError($"{typeof(GameController)} is needed in the scene but it does not exist!");
                    }
                }
                return _instance;
            }
        }

        #endregion
    }
}