using UnityEngine;
using System.Collections;
using System;

namespace MiniGame
{

    public class MoveAround : MonoBehaviour
    {
        // 移动速度
        public float speed = 10;
        // 是否来回运动
        public bool isOscillatory = true;
        // 最大次数，负数为无限次
        public int maxinumTimes = -1;
        // 起始点
        public Transform startPoint;
        // 终点
        public Transform endPoint;

        // 减速距离
        public float speedDownDistance = 0.0f;
        // 最小速度
        public float minSpeed = 3.0f;

        //GameObject移动时候的位置    
        private Transform _cachedTransform;
        private Vector3 _startPosition;
        private Vector3 _endPosition;
        private Vector3 _prevPosition;
        // 剩余次数
        private int _remainTimes;


        // 当前速度
        private float _currentSpeed;
        // 起点终点附近加速度
        private float _acceleration;

        private void Awake()
        {
            _remainTimes = maxinumTimes;
            _startPosition = startPoint.transform.position;
            _endPosition = endPoint.transform.position;
            _cachedTransform = gameObject.transform;
            _currentSpeed = speed;
            if (speedDownDistance < float.Epsilon)
            {
                _acceleration = -1;
            } else
            {
                _acceleration = (speed * speed) / speedDownDistance;
            }
        }

        private void Update()
        {
            if (_remainTimes == 0)
            {
                return;
            }
            MoveObj();
            if (CheckHasReachedEnd())
            {
                ResetPosition();
                _remainTimes--;
            }
        }

        private void MoveObj()
        {
            var direction = (_endPosition - _cachedTransform.position).normalized;
            _cachedTransform.position += direction * _currentSpeed * Time.deltaTime;
            if (_acceleration > float.Epsilon)
            {
                if (Vector3.Distance(_cachedTransform.position, _endPosition) <= speedDownDistance)
                {
                    _currentSpeed = Math.Max(_currentSpeed - _acceleration * Time.deltaTime, minSpeed);
                }
                if (Vector3.Distance(_cachedTransform.position, _startPosition) <= speedDownDistance)
                {
                    _currentSpeed = Math.Min(_currentSpeed + _acceleration * Time.deltaTime, speed);
                }
            }
        }

        private bool CheckHasReachedEnd()
        {
            return 
                Vector3.Distance(_cachedTransform.position, _startPosition) >= 
                Vector3.Distance(_endPosition, _startPosition);
        }
        
        private void ResetPosition()
        {
            if (isOscillatory)
            {
                var temp = _startPosition;
                _startPosition = _endPosition;
                _endPosition = temp;
            }
            _cachedTransform.position = _startPosition;
            if (_acceleration > float.Epsilon)
            {
                _currentSpeed = minSpeed;
            } else
            {
                _currentSpeed = speed;
            }
        }
    }
}
