using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
    public ManagerStatus mStatus
    {
        get;
        private set;
    }

    /// <summary>
    /// 启动
    /// </summary>
    public void Startup()
    {
        mStatus = ManagerStatus.Started;
    }

    /// <summary>
    /// 更新Player的位置
    /// </summary>
    /// <param name="transform"></param>
    public void UpdatePosition(Transform transform)
    {

    }
}
