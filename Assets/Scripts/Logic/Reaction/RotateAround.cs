using UnityEngine;
using System.Collections;

namespace MiniGame
{
    /// <summary>
    /// 绕着某个点来回旋转，石头的旋转效果
    /// </summary>
    public class RotateAround: MonoBehaviour
    {
        //围绕旋转的中心点
        public Transform centerPoint;

        public bool isClockWise = true;
        //绕着中心点旋转的速度
        public float angularSpeed = 10;

        private Vector3 centerPointPosition;
        //GameObject移动时候的位置    
        private Transform cachedTransform;
        //旋转的方向
        private float rotatedDir;


        private void Awake()
        {
            centerPointPosition = centerPoint.transform.position;
            cachedTransform = gameObject.transform;
            gameObject.transform.up = (cachedTransform.position - centerPointPosition).normalized;

            if (isClockWise)
            {
                rotatedDir = -1;
            }
            else
            {
                rotatedDir = 1;
            }
        }

        private void Update()
        {
            rotateObj();
        }

        private void rotateObj()
        {           
            cachedTransform.RotateAround(centerPointPosition, rotatedDir * Vector3.forward, Time.deltaTime * angularSpeed);
        }
    }
}

