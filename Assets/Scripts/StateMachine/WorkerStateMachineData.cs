using System;
using UnityEngine;

namespace Assets.Scripts.StateMachine
{
    public class WorkerStateMachineData
    {
        private float _speed = 10f;
        private float _minDistanceToResource = 3f;
        private float _minDistanceToMotherbase = 2f;

        public float Speed
        {
            get => _speed;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                _speed = value;
            }
        }
        public float DurationLoadingResource { get; private set; } = 1f;
        public float MinDistanceToResource => _minDistanceToResource;
        public float MinDistanceToMotherbase => _minDistanceToMotherbase;
    }
}
