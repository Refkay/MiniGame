using UnityEngine;
using System.Collections;
using System;

//脚本单例基类，这里注意MonoSingleton会在Awake的时候保证单例的唯一性，如果有多创建的则立即销毁
namespace MiniGameComm
{
    /// <summary>
    /// The auto singleton attribute.
    /// </summary>
    public class AutoSingletonAttribute : Attribute
    {
        /// <summary>
        /// The b auto create.
        /// </summary>
        public bool bAutoCreate;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoSingletonAttribute"/> class.
        /// </summary>
        /// <param name="bCreate">
        /// The b create.
        /// </param>
        public AutoSingletonAttribute(bool bCreate)
        {
            this.bAutoCreate = bCreate;
        }
    }


    /// <summary>
    /// 基类继承树中有MonoBehavrour类的单件实现，这种单件实现有利于减少对场景树的查询操作
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    [AutoSingleton(true)]
    public class MonoSingleton<T> : MonoBehaviour where T : Component
    {
        // 单件子类实例
        /// <summary>
        /// The _instance.
        /// </summary>
        private static Component _instance;

        // 在单件中，每个物件的destroyed标志设计上应该分割在不同的存储个空间中，因此，忽略R#的这个提示
        // ReSharper disable once StaticFieldInGenericType
        /// <summary>
        /// The _destroyed.
        /// </summary>
        private static bool _destroyed;

        /// <summary>
        ///     获得单件实例，查询场景中是否有该种类型，如果有存储静态变量，如果没有，构建一个带有这个component的gameobject
        ///     这种单件实例的GameObject直接挂接在bootroot节点下，在场景中的生命周期和游戏生命周期相同，创建这个单件实例的模块
        ///     必须通过DestroyInstance自行管理单件的生命周期
        /// </summary>
        /// <returns>返回单件实例</returns>
        public static T GetInstance()
        {
            return (T)_instance;
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static T Instance
        {
            get { return GetInstance(); }
        }

        /// <summary>
        ///     删除单件实例,这种继承关系的单件生命周期应该由模块显示管理
        /// </summary>
        public static void DestroyInstance()
        {
            if (_instance != null)
            {
                MonoBehaviour.Destroy(_instance.gameObject);
            }

            _destroyed = true;
            _instance = null;
        }

        /// <summary>
        /// The clear destroy.
        /// </summary>
        public static void ClearDestroy()
        {
            DestroyInstance();

            _destroyed = false;
        }

        /// <summary>
        ///     Awake消息，确保单件实例的唯一性
        /// </summary>
        protected virtual void Awake()
        {
            //DebugHelper.Log("Awake :" + typeof(T).ToString());
            if (_instance != null && _instance.gameObject != this.gameObject)
            {
                if (Application.isPlaying)
                {
                    MonoBehaviour.Destroy(this.gameObject);
                }
                else
                {
                    MonoBehaviour.DestroyImmediate(this.gameObject); // UNITY_EDITOR
                }
            }
            else if (_instance == null)
            {
                DontDestroyUtil.DontDestroyOnLoadWrapper(this.gameObject);
                _instance = this;

                this.Init();
            }
        }

        /// <summary>
        ///     OnDestroy消息，确保单件的静态实例会随着GameObject销毁
        /// </summary>
        protected void OnDestroy()
        {
            if (_instance != null && _instance.gameObject == this.gameObject)
            {
                _instance = null;

                // 反初始化
                this.Uninit();
            }
        }

        /// <summary>
        /// The destroy self.
        /// </summary>
        public virtual void DestroySelf()
        {
            _instance = null;
            MonoBehaviour.Destroy(this.gameObject);
        }

        /// <summary>
        /// The has instance.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool HasInstance()
        {
            return _instance != null;
        }

        /// <summary>
        /// The init.
        /// </summary>
        protected virtual void Init()
        {

        }

        protected virtual void Uninit()
        {

        }
    }

}

