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
        [SerializeField] private WheelCollider[] _wheels;

        private Vector3 _checkPoint;

        public static Vector3 ForwardVector;

        private void Awake()
        {
            Initialize();
            _checkPoint = transform.position;

            CheckPointTriggerer.CheckPointTriggered += OnCheckPointTriggered;
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
            transform.position += Vector3.up * 2;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            if (_rigidBody.isKinematic) _rigidBody.isKinematic = false;

            _carModel.CreateMesh(controlPoints);
            _carModel.PlaceWheels();
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

        private void FixedUpdate()
        {
            if (_rigidBody.velocity.magnitude < _maxSpeed)
            {
                foreach (var wheel in _wheels)
                {
                    wheel.motorTorque = Time.deltaTime * _torque;
                }
                if (IsGround())
                {
                    _rigidBody.AddForce(transform.forward * Time.deltaTime* 0.1f * _hp, ForceMode.Acceleration);
                }
            }
            if (_rigidBody.angularVelocity.magnitude > 5)
            {
                _rigidBody.angularVelocity = Vector3.Lerp(_rigidBody.angularVelocity, Vector3.zero, Time.deltaTime * 10f);
            }

            Debug.DrawRay(transform.position, transform.forward * 1000, Color.red, Time.fixedDeltaTime);
        }

        public void Boost()
        {
            StartCoroutine(BoostRoutine());

            IEnumerator BoostRoutine(float timer = 0.5f)
            {
                _maxSpeed *= 2;
                while (timer > 0)
                {
                    _rigidBody.AddForce(Vector3.Lerp(_rigidBody.velocity, transform.forward * 10, Time.deltaTime * 1000), ForceMode.VelocityChange);
                    timer--;
                    yield return null;
                }
                _maxSpeed /= 2;
            }
        }

        public void Respawn()
        {
            transform.position = _checkPoint;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            _rigidBody.isKinematic = true;
            _rigidBody.isKinematic = false;
        }

        private void OnCheckPointTriggered(Vector3 checkpointPosition)
        {
            _checkPoint = checkpointPosition;
        }

    }
}