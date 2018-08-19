using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有Manager都是实现这个接口
/// </summary>
public interface IGameManager  {

	ManagerStatus mStatus { get; }

    /// <summary>
    /// 启动
    /// </summary>
    void Startup();
}
