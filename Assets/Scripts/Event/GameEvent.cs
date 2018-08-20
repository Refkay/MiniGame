using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame
{
    /// <summary>
    /// 大关卡通关成功的消息
    /// </summary>
    public class OnLevelCompleteMsg
    {

    }  

    /// <summary>
    /// 小关卡通关成功的消息
    /// </summary>
    public class OnSubLevelCompleteMsg
    {

    }

    /// <summary>
    /// 小关卡通关失败的消息
    /// </summary>
    public class OnSubLevelFailedMsg
    {

    }

    /// <summary>
    /// 所有关卡通关成功的消息
    /// </summary>
    public class OnGameCompleteMsg
    {

    }

    /// <summary>
    /// 相机移动
    /// </summary>
    public class OnCameraMoveMsg
    {
        public Vector3 mPosition;

        public bool isMoveDirectly = false;

        public OnCameraMoveMsg(Vector3 position, bool directly)
        {
            this.mPosition = position;
            isMoveDirectly = directly;
        }
    }

    /// <summary>
    /// 光球位置改变
    /// </summary>
    public class OnPlayerPosChangeMsg
    {
        public Vector3 mPosition;

        public OnPlayerPosChangeMsg(Vector3 position)
        {
            this.mPosition = position;
        }
    }
}

