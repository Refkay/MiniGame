using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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
    /// 吃到中转点，能量水晶的消息
    /// </summary>
    public class OnAddTurnChanceMsg
    {

    }

    /// <summary>
    /// 吃到水晶的消息
    /// </summary>
    public class OnAddStarMsg
    {

    }

    /// <summary>
    /// 相机移动
    /// </summary>
    public class OnCameraMoveMsg
    {
        public Vector3 mPosition;

        public bool isMoveDirectly = false;

        public OnCameraMoveMsg(Vector3 position,bool directly)
        {
            this.mPosition = position;
            this.isMoveDirectly = directly;
        }
    }

    /// <summary>
    /// 光球移动到指定位置
    /// </summary>
    public class OnPlayerMoveMsg
    {
        public Vector3 mTargetPos;

        public OnPlayerMoveMsg(Vector3 position)
        {
            this.mTargetPos = position;
        }
    }
}

