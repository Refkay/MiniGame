using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;
using UnityEngine.UI;
using MiniGameComm;
using DG.Tweening;

namespace MiniGame
{
    public class Move : MonoBehaviour
    {

        public Rigidbody2D mRg2D;

        //能否能移动
        private bool isMoveable;

        private Vector2 mStartPosition;

        private Vector2 mTargetPosition;

        private Vector2 mMoveDirection;

        //物体移动的时间
        [SerializeField]
        private float mMoveSpeed = 7.0f;

        //滑动时间
        private float mDragTime = 0.0f;

        //滑动时间的阈值
        [SerializeField]
        private float mDragTimeThreshold = 0.5f;

        //滑动距离
        private float mOffsetDistance = 0.0f;

        //滑动距离的阈值
        [SerializeField]
        private float mOffsetThreshold = 100.0f;

        //最小可转向距离，滑动必须超过这个距离才可以进行滑动
        [SerializeField]
        private float mMinoffset = 30.0f;

        private Vector3 velocity = Vector3.one;

        //是否加速
        private bool isAccelerate;

        //加速速度，进入加速区时Player的移动速度变成加速速度
        [SerializeField]
        private float mAccSpeed = 3.0f;

        //光球初始化位置
        private Vector3 mInitialPosition;

        //光球初始化
        private Quaternion mInitialRotation;

        //转向机会
        private int mTurnCount = 2;

        //吃到的星星数量
        private int mStartCount = 1;

        void Awake()
        {
            InitMessage(true);
            Config config = Config.Load();
            if (config != null)
            {
                mMoveSpeed = config.moveSpeed;
                mDragTimeThreshold = config.dragTimeThreshold;
                mOffsetThreshold = config.offsetThreshold;
            }        
        }

        void Start()
        {          
            mRg2D = GetComponent<Rigidbody2D>();
            mInitialPosition = transform.position;
            mInitialRotation = transform.rotation;
        }

        private void InitMessage(bool register)
        {
            if (register)
            {
                MessageBus.Register<OnAddTurnChanceMsg>(OnAddTurnChance);
                MessageBus.Register<OnAddStarMsg>(OnAddStar);
            }
            else
            {
                MessageBus.UnRegister<OnAddTurnChanceMsg>(OnAddTurnChance);
                MessageBus.UnRegister<OnAddStarMsg>(OnAddStar);
            }
        }

        /// <summary>
        /// 吃到能量水晶增加转向机会
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool OnAddTurnChance(OnAddTurnChanceMsg msg)
        {
            mTurnCount++;
            return false;
        }

        /// <summary>
        /// 吃到星星
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool OnAddStar(OnAddStarMsg msg)
        {
            mStartCount++;
            return false;
        }

        private void OnEnable()
        { 
            EasyTouch.On_TouchStart += On_TouchStart;
            EasyTouch.On_TouchDown += On_TouchDown;
            EasyTouch.On_TouchUp += On_TouchUp;
        }

        private void OnDestroy()
        {
            InitMessage(false);
            EasyTouch.On_TouchStart -= On_TouchStart;
            EasyTouch.On_TouchDown -= On_TouchDown;
            EasyTouch.On_TouchUp -= On_TouchUp;
        }

        private void On_TouchStart(Gesture gesture)
        {
            mStartPosition = gesture.position;
        }

        private void On_TouchDown(Gesture gesture)
        {
            mTargetPosition = gesture.position;
            mMoveDirection = mTargetPosition - mStartPosition;
            mDragTime += Time.deltaTime;
            mOffsetDistance = mMoveDirection.magnitude;
            //滑动的时候超过时间的阈值或者超过距离的阈值，球也要转向      
            if (((mDragTime > mDragTimeThreshold) || (mOffsetDistance > mOffsetThreshold)) && isMoveable )
            {
                mMoveDirection.Normalize();
                mRg2D.velocity = mMoveDirection * mMoveSpeed;
                mDragTime = 0;
                mOffsetDistance = 0;
                isMoveable = false;
                mTurnCount--;
            }
        }

        private void On_TouchUp(Gesture gesture)
        {
            //滑动时间归0
            mDragTime = 0;
            //偏移量归0
            mOffsetDistance = 0;
            mTargetPosition = gesture.position;
            mMoveDirection = mTargetPosition - mStartPosition;
            //必须滑动到一定的距离才能转向
            if (mMoveDirection.magnitude >= mMinoffset )
            {
                mRg2D.Sleep();
                mMoveDirection.Normalize();
                //mRg2d.AddForce(moveDirection * mMoveSpeed);
                mRg2D.velocity = mMoveDirection * mMoveSpeed;
                isMoveable = true;
                mTurnCount--;
            }
        }       

        /// <summary>
        /// 死亡
        /// </summary>
        private void OnPlayerDead()
        {
            //重置位置
            gameObject.transform.position = mInitialPosition;
            gameObject.transform.rotation = mInitialRotation;
            mRg2D.Sleep();
            //重置转向次数
            mTurnCount = 2;
            MessageBus.Send(new OnSubLevelFailedMsg());
        }

        /// <summary>
        /// 碰撞检测
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {           
            string tag = collision.gameObject.tag;
            switch (tag)
            {
                case "Damage":
                    OnPlayerDead();                   
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 接触到触发器
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            string tag = collision.gameObject.tag;
            switch (tag)
            {
                case "Destination":
                    //通过当前小关卡
                    Debug.Log("On SubLevel Complete");
                    GetComponent<Rigidbody2D>().Sleep();
                    //发送消息到GameSystem，小关卡完成，通知进入下一关卡,这里传入了一个位置参数，这样子只需要保存一个位置就够了
                    MessageBus.Send(new OnSubLevelCompleteMsg());
                    Destroy(collision.gameObject);              
                    break;
                default:
                    break;
            }     
        }

        /// <summary>
        /// 在触发器里面，主要是障碍里面的加速区域和引力区域
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerStay2D(Collider2D collision)
        {
            string tag = collision.gameObject.tag;
            switch (tag)
            {
                case "Accelerate":
                    //进入加速区域
                    mRg2D.velocity = mMoveDirection * mAccSpeed;
                    break;
                default:
                    break;
            }        
        }

        /// <summary>
        /// 出触发器里面出来
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerExit2D(Collider2D collision)
        {
            string tag = collision.gameObject.tag;
            switch (tag)
            {
                case "Accelerate":
                    //从加速区域出来，速度要恢复成原来的速度
                    mRg2D.velocity = mMoveDirection * mMoveSpeed;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 物体在摄像机内
        /// </summary>
        private void OnBecameVisible()
        {

        }

        /// <summary>
        /// 不在摄像机内触发改方法，Player一移动出摄像机就判定为死亡
        /// </summary>
        private void OnBecameInvisible()
        {
            OnPlayerDead();
        }
     
    }

}
