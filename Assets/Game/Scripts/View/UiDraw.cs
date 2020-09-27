using Game.Scripts.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using Game.Scripts.Extensions;
using PathCreation;
using ProBuilder2.Common;
using System.Linq;

namespace Game.Scripts.View
{
    public delegate void LineDrawDelegate(List<Vector2> controlPoints);
    public class UiDraw : MonoBehaviour
    {
        public static event LineDrawDelegate LineDrew;

        [SerializeField] private InputController _input;
        [SerializeField] private UILineTextureRenderer _lineRenderer;

        public float TwoPointMaxDis = 50f;

        private List<Vector2> _touchPositions = new List<Vector2>();

        private RectTransform _rectTransform;

        private void Start()
        {
            _rectTransform = transform as RectTransform;
        }
        private void Update()
        {
            if (_input.Active)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    DrawLine();
                }
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    UpdateLine();
                }
            }
            else
            {
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    if (_touchPositions.Count > 5)
                    {
                        LineDrew?.Invoke(_touchPositions);
                    }
                    else
                    {
                        Time.timeScale = 1;
                    }
                }
            }
        }

        private void DrawLine()
        {
            _touchPositions.Clear();
            Vector2 firstPosition = _input.StartPosition.InversePoint(_lineRenderer.transform);
            _touchPositions.Add(firstPosition);
            _lineRenderer.Points = _touchPositions.ToArray();
        }

        private void UpdateLine()
        {
            Vector2 touchPos = _input.CurrentPosition.InversePoint(_lineRenderer.transform);

            var heightClamp = _rectTransform.rect.height / 2f;
            var widthClamp = Screen.width / 2f;
            touchPos = new Vector2(Mathf.Clamp(touchPos.x, widthClamp * -1f, widthClamp), Mathf.Clamp(touchPos.y, heightClamp * -1f, heightClamp));

            float distance = Vector2.Distance(_touchPositions[_touchPositions.Count - 1], touchPos);
            if (distance > TwoPointMaxDis)
            {
                _touchPositions.Add(touchPos);
                _lineRenderer.Points = _touchPositions.ToArray();
            }
        }
    }
}