using Game.Scripts.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.View
{
    public class InGameView : View
    {
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

    public class InGameViewParameters : ViewParameters { }
}