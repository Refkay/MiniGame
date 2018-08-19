using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 障碍物管理类
/// </summary>
public class ObstacleManager : MonoBehaviour, IGameManager {
    public ManagerStatus mStatus
    {
        get;
        private set;
    }

    public void Startup()
    {
        mStatus = ManagerStatus.Started;
    }
   
}
