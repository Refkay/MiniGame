using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniGameComm;
using DG.Tweening;

namespace MiniGame
{
    public class CameraMoveControl : MonoBehaviour
    {
        //相机移动时间
        private const float MOVE_TIME = 1.5f;

        void Awake()
        {
            gameObject.transform.position = MissionManager.Instance.GetCameraStartPos();
            MessageBus.Register<OnCameraMoveMsg>(OnCameraMove);
        }

        private void OnDestroy()
        {
            MessageBus.UnRegister<OnCameraMoveMsg>(OnCameraMove);
        }

        //移动相机到指定位置
        private bool OnCameraMove(OnCameraMoveMsg msg)
        {
            Vector3 targetPosition = new Vector3(msg.mPosition.x, msg.mPosition.y, gameObject.transform.position.z);
            //相机直接移动到某个位置，没有过度动画
            if (msg.isMoveDirectly)
            {
                GetComponent<Camera>().transform.position = targetPosition;
            }
            else
            {
                //Vector3 rotate = new Vector3(0, 0, msg.mAngle);
                //GetComponent<Camera>().transform.DORotate(rotate, 1.0f);               
                GetComponent<Camera>().transform.DOMove(targetPosition, 4.0f);
            }

            return false;
        }


    }
}

