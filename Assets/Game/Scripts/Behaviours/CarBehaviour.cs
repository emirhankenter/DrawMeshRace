using Game.Scripts.View;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Scripts.Extensions;

namespace Game.Scripts.Behaviours
{
    public class CarBehaviour : MonoBehaviour
    {
        [SerializeField] private float _torque;
        [SerializeField] private float _hp;
        [SerializeField] private float _maxSpeed;

        [SerializeField] private Rigidbody _rigidBody;

        [SerializeField] private CarModel _carModel;
        [SerializeField] private Transform _frontWheels;
        [SerializeField] private Transform _backWheels;
        [SerializeField] private WheelCollider[] _wheels;

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            _carModel.Initialize();
            UiDraw.LineDrew += OnLineDrew;

            StartCoroutine(RideRoutine());
        }

        public void Dispose()
        {
            UiDraw.LineDrew -= OnLineDrew;
        }

        private void OnLineDrew(List<Vector2> controlPoints)
        {
            _carModel.CreateMesh(controlPoints);

            var back = controlPoints.First();
            var front = controlPoints.Last();

            _backWheels.transform.position = new Vector3(0, back.y, back.x) * _carModel.transform.lossyScale.x;
            _frontWheels.transform.position = new Vector3(0, front.y, front.x) * _carModel.transform.lossyScale.x;
        }

        private bool IsGround()
        {
            foreach (var wheel in _wheels)
            {
                if (wheel.isGrounded)
                {
                    return true;
                }
            }
            return false;
        }

        private IEnumerator RideRoutine()
        {
            while (true)
            {
                foreach (var wheel in _wheels)
                {
                    //wheel.motorTorque = Time.fixedDeltaTime * _torque;
                }

                yield return new WaitForFixedUpdate();
            }
        }

        private void FixedUpdate()
        {

            if (_rigidBody.velocity.magnitude < _maxSpeed)
            {
                foreach (var item in _wheels)
                {
                    item.motorTorque = Time.deltaTime * _torque;
                }
                if (IsGround())
                {
                    _rigidBody.AddForce(Vector3.forward * Time.deltaTime* 0.1f * _hp, ForceMode.Acceleration);
                }
            }
            if (_rigidBody.angularVelocity.magnitude > 5)
            {
                _rigidBody.angularVelocity = Vector3.Lerp(_rigidBody.angularVelocity, Vector3.zero, Time.deltaTime * 10f);
            }
        }

    }
}