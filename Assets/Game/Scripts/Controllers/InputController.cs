using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts.Controllers
{
    public class InputController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        public bool Active;
        public PointerEventData EventData;

        private Vector2 _startPosition;
        private Vector2 _endPosition;
        private Vector2 _currentPosition;
        private Vector2 _deltaPosition;

        public Vector2 StartPosition => _startPosition;
        public Vector2 EndPosition => _endPosition;
        public Vector2 CurrentPosition => _currentPosition;
        public Vector2 DeltaPosition => _deltaPosition;

        public void OnPointerDown(PointerEventData eventData)
        {
            EventData = eventData;
            _startPosition = eventData.position;
            _currentPosition = eventData.position;
            Active = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _deltaPosition = eventData.delta;
            _currentPosition = eventData.position;
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            Active = false;
            _endPosition = eventData.position;
        }
    }
}