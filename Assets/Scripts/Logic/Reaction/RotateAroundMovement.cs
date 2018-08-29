using UnityEngine;
using System.Collections;

namespace MiniGame
{
    /// <summary>
    /// 绕着某个点来回旋转，石头的旋转效果
    /// </summary>
    public class RotateAroundMovement : MonoBehaviour
    {
        //围绕旋转的中心点
        public Transform centerPoint;
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

            rotatedDir = -1;
        }

        private void Update()
        {
            rotateObj();
        }

        private void rotateObj()
        {
            //中心到物体的向量
            Vector3 posDir = cachedTransform.position - centerPointPosition;
            //向量跟物体y轴的夹角，判断是否大于0，对应的是Cos函数的值,这样子让物体在90度之间来回绕着中心点旋转
            if (Vector3.Dot(posDir, cachedTransform.up) > 0)
            {
                //顺时针旋转
                rotatedDir = -1;
            }
            else
            {
                //逆时针旋转
                rotatedDir = 1;
            }

            cachedTransform.RotateAround(centerPointPosition, rotatedDir * Vector3.forward, Time.deltaTime * angularSpeed);
        }
    }
}

