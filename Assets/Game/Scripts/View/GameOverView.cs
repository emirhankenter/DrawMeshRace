using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.View
{
    public class GameOverView : View
    {
        private GameOverViewParameters _params;

        public override void Open(ViewParameters parameters)
        {
            base.Open();

            _params = parameters as GameOverViewParameters;
            if (_params == null) return; 

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
        }

        private void UnregisterEvents()
        {
        }
        private void InitializeElements()
        {
        }

        private void DisposeElements()
        {
        }
    }

    public class GameOverViewParameters : ViewParameters 
    {
    }
}