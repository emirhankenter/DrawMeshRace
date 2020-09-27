using Assets.Game.Scripts.Behaviours;
using Game.Scripts.Behaviours;
using Game.Scripts.Controllers;
using Game.Scripts.Models;
using Game.Scripts.View.Elements;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.View
{
    public class InGameView : View
    {
        [SerializeField] private UiDraw _uiDrawView;
        [SerializeField] private CoinElement _coinElement;

        public override void Open(ViewParameters parameters)
        {
            base.Open();

            InitializeElements();

            RegisterEvents();
        }

        public override void Close()
        {
            base.Close();

            DisposeElements();

            UnregisterEvents();
        }

        private void RegisterEvents()
        {
            CoinBehaviour.CoinCollected += OnCoinCollected;
            FinishLineBehaviour.FinishLinePassed += OnFinishLinePassed;
        }

        private void OnCoinCollected()
        {
            PlayerData.Coin += GameConfig.CollectibleCoinRewardAmount;
            _coinElement.UpdateCoin(PlayerData.Coin);
        }

        private void OnFinishLinePassed()
        {
            _uiDrawView.gameObject.SetActive(false);
        }

        private void UnregisterEvents()
        {
            CoinBehaviour.CoinCollected -= OnCoinCollected;
            FinishLineBehaviour.FinishLinePassed += OnFinishLinePassed;
        }
        private void InitializeElements()
        {
            _uiDrawView.gameObject.SetActive(true);
            _coinElement.Initialize(PlayerData.Coin);
        }

        private void DisposeElements()
        {
        }
    }

    public class InGameViewParameters : ViewParameters { }
}