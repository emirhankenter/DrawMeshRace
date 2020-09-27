using Game.Scripts.Behaviours;
using Game.Scripts.View;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mek.Controllers;

namespace Assets.Game.Scripts.Controllers
{
    public class GameController : MonoBehaviour
    {
        public static event Action GameStarted;
        public static event Action GameOver;

        public LevelBehaviour CurrentLevel;

        [SerializeField] private List<LevelBehaviour> _levels;

        private void Awake()
        {
            PrepareLevel();
        }

        private void PrepareLevel()
        {
            CurrentLevel = Instantiate(_levels[0]);
            CurrentLevel.Initialize();

            GameStarted?.Invoke();

            ViewController.Instance.InGameView.Open(new InGameViewParameters());
        }

        private void DisposeLevel()
        {

            Destroy(CurrentLevel.gameObject);

            CoroutineController.DoAfterGivenTime(1f, () =>
            {
                ViewController.Instance.GameOverView.Close();
                PrepareLevel();
            });
        }

        private void OnGameOver()
        {
            ViewController.Instance.InGameView.Close();
            ViewController.Instance.GameOverView.Open(new GameOverViewParameters());
            GameOver?.Invoke();
        }

        private void OnRestart()
        {
            DisposeLevel();
        }

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