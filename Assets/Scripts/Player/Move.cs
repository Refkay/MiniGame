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
        public ParticleSystem mExplosionParticles;

        public Rigidbody2D mRg2D;

        private GameObject mPlayerDead;

        //能否能移动
        private bool isMoveable = true;

        private Vector3 mStartPosition;

        private Vector3 mTargetPosition;

        private Vector3 mMoveDirection;

        //物体移动的速度
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
        private float mAccSpeed = 10.0f;

        //光球初始化位置
        private Vector3 mInitialPosition;

        //光球初始化角度
        private Quaternion mInitialRotation;

        //转向机会
        private int mTurnCount = 2;

        //吃到的星星数量
        private int mStartCount = 1;

        //是否通关了
        private bool isSuccess;

        //小球重生时间
        private float mRebornTime = 1.1f;

        //能否播放小球死亡动画
        private bool canPlayDeadEffet = true;

        //初始化速度
        private float mInitialSpeed;

        //能否加速
        private bool canAccelerate = false;

        private bool canTurnInfinite;

        private bool canCountDownTurn = true;

        private bool canPlayMoveSound = true;

        private bool canDoSuccess = true;

        void Awake()
        {
            gameObject.transform.position = MissionManager.Instance.GetPlayerStartPos();
            InitMessage(true);
        }

        void Start()
        {
            mRg2D = GetComponent<Rigidbody2D>();
            mInitialPosition = transform.position;
            mInitialRotation = transform.rotation;
            mInitialSpeed = mMoveSpeed;
            canTurnInfinite = MissionData.GetLevelTurnInfinite(MissionManager.Instance.mCurLevel, MissionManager.Instance.mCurSubLevel);
            isSuccess = false;
            StartCoroutine(ShowGamePanel());
        }


        IEnumerator ShowGamePanel()
        {
            yield return new WaitForSeconds(3.0f);
            UIManager.Instance.GoToGame();
        }
    
        private void InitMessage(bool register)
        {
            if (register)
            {
                MessageBus.Register<OnAddTurnChanceMsg>(OnAddTurnChance);
                MessageBus.Register<OnAddStarMsg>(OnAddStar);
                MessageBus.Register<OnPlayerMoveMsg>(OnPlayerMove);
            }
            else
            {
                MessageBus.UnRegister<OnAddTurnChanceMsg>(OnAddTurnChance);
                MessageBus.UnRegister<OnAddStarMsg>(OnAddStar);
                MessageBus.UnRegister<OnPlayerMoveMsg>(OnPlayerMove);
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

        /// <summary>
        /// Player移动到指定位置
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool OnPlayerMove(OnPlayerMoveMsg msg)
        {
            isSuccess = false;
            canTurnInfinite = MissionData.GetLevelTurnInfinite(MissionManager.Instance.mCurLevel, MissionManager.Instance.mCurSubLevel);
            mInitialPosition = msg.mTargetPos;
            if (msg.isMoveDirectly) {
                gameObject.transform.position = msg.mTargetPos;
            }
            else
            {
                SetPlayerAngle(msg.mTargetPos - gameObject.transform.position);
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                gameObject.transform.Find("Particle System").transform.gameObject.SetActive(true);
                gameObject.transform.DOMove(msg.mTargetPos, 4.0f).OnComplete(() =>
                {                 
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    isMoveable = true;
                    mTurnCount = 2;
                    canCountDownTurn = true;
                    canPlayMoveSound = true;
                    canDoSuccess = true;
                    this.transform.eulerAngles = new Vector3(0, 0, 0);                    
                });
            }
            //小球的初始化位置变为下一个位置         

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
            isMoveable = true;
            mStartPosition = gesture.position;
            Debug.Log("坐标是多少 ： " + gesture.position.x + "  Y: " + gesture.position.y);
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
                if (canTurnInfinite || mTurnCount > 0)
                {
                    if (canPlayMoveSound)
                    {
                        AudioManager.Instance.PlayOneShotIndex(6);
                        canPlayMoveSound = false;
                    }       
                    SetPlayerAngle(mMoveDirection);
                    mMoveDirection.Normalize();
                    mRg2D.velocity = mMoveDirection * mMoveSpeed;
                    mDragTime = 0;
                    mOffsetDistance = 0;                
                    canAccelerate = true;
                    if (canCountDownTurn)
                    {
                        mTurnCount--;
                    }
                    isMoveable = false;  
                }
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
            if (mMoveDirection.magnitude >= mMinoffset && isMoveable)
            {
                if (canTurnInfinite || mTurnCount > 0)
                {
                    if (canPlayMoveSound)
                    {
                        AudioManager.Instance.PlayOneShotIndex(6);
                        canPlayMoveSound = false;
                    }
                    mRg2D.Sleep();
                    SetPlayerAngle(mMoveDirection);
                    mMoveDirection.Normalize();
                    //mRg2d.AddForce(moveDirection * mMoveSpeed);
                    mRg2D.velocity = mMoveDirection * mMoveSpeed;
                    canAccelerate = true;
                    if (canCountDownTurn)
                    {
                        mTurnCount--;
                    }
                    isMoveable = false;
                }
            }
        }       

        private void SetPlayerAngle(Vector3 targetPosition)
        {
            float angle = Vector2.Angle(targetPosition, Vector3.up);
            if (targetPosition.x > gameObject.transform.position.x)
            {
                angle = -angle;
            }
            this.transform.eulerAngles = new Vector3(0, 0, angle);
        }

        /// <summary>
        /// 死亡
        /// </summary>
        private void OnPlayerDead(bool isOutOfCamera)
        {
            //重置位置                      
            mRg2D.Sleep();
            isMoveable = false;
            canCountDownTurn = false;
            canPlayMoveSound = true;
            //播放死亡动画         
            mExplosionParticles.Play();
            //光球出摄像机外不播放死亡动画
            if (!isOutOfCamera && canPlayDeadEffet)
            {
                //mPlayerDead = GameObject.Instantiate(Resources.Load("Prefabs/PlayerDead")) as GameObject;
                //mPlayerDead.transform.position = gameObject.transform.position;
                
                canPlayDeadEffet = false;
                //播放死亡音效  
                AudioManager.Instance.PlayOneShotIndex(7);
            }    
            else
            {
                AudioManager.Instance.PlayOneShotIndex(10);
            }
            //这里是隐藏光球，因为Invoke或者协程需要gameObject的active为true的时候才能正常执行，所以这里想到用改透明度来隐藏光球
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            gameObject.transform.Find("Particle System").transform.gameObject.SetActive(false);
            canAccelerate = false;         
            Invoke("ResetPlayer", mRebornTime);         
            Debug.Log("执行到这里了");
  
        }

        //重置小球
        private void ResetPlayer()
        {
            gameObject.transform.position = mInitialPosition;
            gameObject.transform.rotation = mInitialRotation;
            gameObject.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 1), 1.0f) ;
            gameObject.transform.Find("Particle System").transform.gameObject.SetActive(true);
            this.transform.eulerAngles = new Vector3(0, 0, 0);
            isMoveable = true;
            canPlayDeadEffet = true;
            canAccelerate = false;
            mMoveSpeed = mInitialSpeed;
            //销毁小球死亡的GameObject
            if (mPlayerDead != null)
            {
                Destroy(mPlayerDead);
            }
            //重置转向次数
            mTurnCount = 2;
            canCountDownTurn = true;
            MessageBus.Send(new OnSubLevelFailedMsg());
        }

        private IEnumerator GoToNext()
        {
            yield return new WaitForSeconds(1.2f);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;          
            canAccelerate = false;
            mMoveSpeed = mInitialSpeed;           
            //发送消息到GameSystem，小关卡完成，通知进入下一关卡      
            MessageBus.Send(new OnSubLevelCompleteMsg());
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
                    OnPlayerDead(false);       
                    break;
                //case "Accelerate":
                //    //进入加速区域
                //    mMoveDirection.Normalize();
                //    mRg2D.velocity = mMoveDirection * mAccSpeed;
                //    mMoveSpeed = mAccSpeed;
                //    break;
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
                    if (canDoSuccess)
                    {
                        canDoSuccess = false;
                        //通过当前小关卡
                        Debug.Log("On SubLevel Complete");
                        AudioManager.Instance.PlayOneShotIndex(8);
                        canCountDownTurn = false;
                        isSuccess = true;
                        isMoveable = false;
                        GetComponent<Rigidbody2D>().Sleep();
                        gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                        gameObject.transform.Find("Particle System").transform.gameObject.SetActive(false);
                        StartCoroutine(GoToNext());
                    }              
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
                    if (canAccelerate)
                    {
                        mMoveDirection.Normalize();
                        mRg2D.velocity = mMoveDirection * mAccSpeed;
                    }          
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
                    if (canAccelerate)
                    {
                        mMoveDirection.Normalize();
                        mRg2D.velocity = mMoveDirection * mMoveSpeed;
                    }                 
                    break;
                case "CameraArea":
                    if (!isSuccess)
                    {
                        OnPlayerDead(true);
                    }
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
            
        }

        private GameObject GetTargetGameObjectByName(GameObject obj,string name)
        {
            foreach (Transform t in obj.GetComponentsInChildren<Transform>())
            {
                if (t.name == name)
                {
                    return t.gameObject;
                }
            }
            return null;
        }
    }

}
