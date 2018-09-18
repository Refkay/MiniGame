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

        //转向机会(注意，真正能转向的次数是这个值减1，因为第一次出发不算转向)
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
        private bool canAccelerate = true;

        private bool canTurnInfinite;

        private bool canCountDownTurn = true;

        private bool canPlayMoveSound = true;

        private bool canDoSuccess = true;

        private bool canMove = true;

        private Vector3 mAcceMovePosition;

        private bool canDreaseTurnCount = false;

        private bool isFlying = false;

        private int mInitinalTurnCount;

        void Awake()
        {
            gameObject.transform.position = MissionManager.Instance.GetPlayerStartPos();
            mInitinalTurnCount = mTurnCount;
            InitMessage(true);
            Config config = Config.Load();
            if (config != null)
            {
                mMoveSpeed = config.moveSpeed;
                mDragTimeThreshold = config.dragTimeThreshold;
                mOffsetThreshold = config.offsetThreshold;
                mAccSpeed = config.accSpeed;
            }
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
            //if (MissionManager.Instance.mCurLevel == 1 && MissionManager.Instance.mCurSubLevel == 1)
            //{
                StartCoroutine(CheckAndShowGuidePanel(MissionManager.Instance.mCurSubLevel, 3.0f));
            //}       
        }


        IEnumerator ShowGamePanel()
        {
            yield return new WaitForSeconds(3.0f);       
            UIManager.Instance.GoToGame();
            UIManager.Instance.SetTurnCountText(mTurnCount - 1);
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
            mTurnCount = mInitinalTurnCount - 1;
            UIManager.Instance.SetTurnCountText(mTurnCount);
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
            if (msg.isMoveDirectly)
            {
                gameObject.transform.position = msg.mTargetPos;
            }
            else
            {
                SetPlayerAngle(msg.mTargetPos - gameObject.transform.position, true);
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                gameObject.transform.Find("Particle System").transform.gameObject.SetActive(true);
                gameObject.transform.DOMove(msg.mTargetPos, 4.0f).OnComplete(() =>
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    isMoveable = true;
                    mTurnCount = mInitinalTurnCount;
                    UIManager.Instance.SetTurnCountText(mTurnCount - 1);
                    canCountDownTurn = true;
                    canPlayMoveSound = true;
                    canDoSuccess = true;
                    canMove = true;
                    canDreaseTurnCount = false;
                    this.transform.eulerAngles = new Vector3(0, 0, 0);
                    isFlying = false;
                    StartCoroutine(CheckAndShowGuidePanel(MissionManager.Instance.mCurSubLevel, 3.0f));
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
            if (mTurnCount > 0)
            {
                mStartPosition = gesture.position;
            }      
          
        }

        private void On_TouchDown(Gesture gesture)
        {
            if (mTurnCount > 0)
            {
                mTargetPosition = gesture.position;
                mMoveDirection = mTargetPosition - mStartPosition;
            }                  
            mDragTime += Time.deltaTime;
            mOffsetDistance = mMoveDirection.magnitude;
            //滑动的时候超过时间的阈值或者超过距离的阈值，球也要转向      
            if (((mDragTime > mDragTimeThreshold) || (mOffsetDistance > mOffsetThreshold)) && isMoveable && canMove)
            {
                if (canTurnInfinite || mTurnCount > 0)
                {
                    if (canPlayMoveSound)
                    {
                        AudioManager.Instance.PlayOneShotIndex(6);
                        canPlayMoveSound = false;
                    }
                    if (!isFlying)
                    {
                        SetPlayerAngle(mMoveDirection, false);
                    }       
                    mMoveDirection.Normalize();
                    mRg2D.velocity = mMoveDirection * mMoveSpeed;
                    mDragTime = 0;
                    mOffsetDistance = 0;
                    //canAccelerate = true;
                    if (canCountDownTurn)
                    {
                        mTurnCount--;
                        if (canDreaseTurnCount)
                        {
                            UIManager.Instance.SetTurnCountText(mTurnCount);
                        }
                        canDreaseTurnCount = true;
                    }
                    isMoveable = false;
                    mAcceMovePosition = mMoveDirection;
                }
            }
        }

        private void On_TouchUp(Gesture gesture)
        {
            //滑动时间归0
            mDragTime = 0;
            //偏移量归0
            mOffsetDistance = 0;
            if (mTurnCount > 0)
            {
                mTargetPosition = gesture.position;
                mMoveDirection = mTargetPosition - mStartPosition;
            }       
            //必须滑动到一定的距离才能转向
            if (mMoveDirection.magnitude >= mMinoffset && isMoveable && canMove)
            {
                if (canTurnInfinite || mTurnCount > 0)
                {
                    if (canPlayMoveSound)
                    {
                        AudioManager.Instance.PlayOneShotIndex(6);
                        canPlayMoveSound = false;
                    }
                    if (!isFlying)
                    {
                        SetPlayerAngle(mMoveDirection, false);
                    }
                    mMoveDirection.Normalize();
                    //mRg2d.AddForce(moveDirection * mMoveSpeed);
                    mRg2D.velocity = mMoveDirection * mMoveSpeed;
                    //canAccelerate = true;
                    if (canCountDownTurn)
                    {
                        mTurnCount--;
                        if (canDreaseTurnCount)
                        {
                            UIManager.Instance.SetTurnCountText(mTurnCount);
                        }
                        canDreaseTurnCount = true;
                    }
                    isMoveable = false;
                    mAcceMovePosition = mMoveDirection;
                }
            }
        }

        private void SetPlayerAngle(Vector3 targetPosition, bool isMoveToNext)
        {
            float angle = Vector2.Angle(targetPosition, Vector3.up);      
            if (!isMoveToNext)
            {
                if (mTargetPosition.x > mStartPosition.x)
                {
                    angle = -angle;
                }
            }
            else
            {
                if (targetPosition.x > gameObject.transform.position.x)
                {
                    angle = -angle;
                }
            }
            this.transform.eulerAngles = new Vector3(0, 0, angle);
            //Quaternion targetAngles = Quaternion.Euler(0, 0, angle);
            //gameObject.transform.rotation = targetAngles;
        }

        /// <summary>
        /// 死亡
        /// </summary>
        private void OnPlayerDead(bool isOutOfCamera)
        {
            //重置位置                      
            mRg2D.Sleep();
            canMove = false;
            isMoveable = false;
            canCountDownTurn = false;
            canPlayMoveSound = true;
            canDreaseTurnCount = false;
            //光球出摄像机外不播放死亡动画
            if (!isOutOfCamera && canPlayDeadEffet)
            {
                //mPlayerDead = GameObject.Instantiate(Resources.Load("Prefabs/PlayerDead")) as GameObject;
                //mPlayerDead.transform.position = gameObject.transform.position;
                //播放死亡动画         
                mExplosionParticles.Play();
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
            canAccelerate = true;
            Invoke("ResetPlayer", mRebornTime);
            Debug.Log("执行到这里了");

        }

        //重置小球
        private void ResetPlayer()
        {
            gameObject.transform.position = mInitialPosition;
            gameObject.transform.rotation = mInitialRotation;
            gameObject.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 1), 1.0f);
            gameObject.transform.Find("Particle System").transform.gameObject.SetActive(true);
            this.transform.eulerAngles = new Vector3(0, 0, 0);
            Invoke("SetMoveable", 1.0f);
            canPlayDeadEffet = true;
            canAccelerate = true;
            mMoveSpeed = mInitialSpeed;
            //销毁小球死亡的GameObject
            if (mPlayerDead != null)
            {
                Destroy(mPlayerDead);
            }
            //重置转向次数
            mTurnCount = mInitinalTurnCount;
            UIManager.Instance.SetTurnCountText(mTurnCount - 1);
            canCountDownTurn = true;
            MessageBus.Send(new OnSubLevelFailedMsg());
        }

        private void SetMoveable()
        {
            canMove = true;
            isMoveable = true;
        }

        private IEnumerator GoToNext()
        {
            yield return new WaitForSeconds(1.2f);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            canAccelerate = true;
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
                        isFlying = true;
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
                case "Accelerate":
                    //进入加速区域
                    if (canAccelerate)
                    {                    
                        SetPlayerAngle(mAcceMovePosition, false);
                        mRg2D.velocity = mAcceMovePosition * mAccSpeed;
                        mMoveSpeed = mAccSpeed;
                        canAccelerate = false;
                        //mMoveDirection.Normalize();
                        //SetPlayerAngle(mMoveDirection, false);
                        //mRg2D.velocity = mMoveDirection * mAccSpeed;
                        //mMoveSpeed = mAccSpeed;
                        //canAccelerate = false;
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
                    //if (canAccelerate && isMoveable)
                    //{
                    //    mMoveDirection.Normalize();
                    //    SetPlayerAngle(mMoveDirection, false);
                    //    mRg2D.velocity = mMoveDirection * mAccSpeed;
                    //    mMoveSpeed = mAccSpeed;
                    //    canAccelerate = false;
                    //}
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
                    //if (isMoveable)
                    //{
                        //从加速区域出来，速度要恢复成原来的速度     
                    if (canMove)
                    {
                        SetPlayerAngle(mAcceMovePosition, false);
                        mRg2D.velocity = mAcceMovePosition * mInitialSpeed;
                        mMoveSpeed = mInitialSpeed;
                        canAccelerate = true;
                    }                       
                    //}
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

        private GameObject GetTargetGameObjectByName(GameObject obj, string name)
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

        IEnumerator CheckAndShowGuidePanel(int curSubLevel, float hideTime)
        {
            yield return null;

            switch (curSubLevel)
            {           
                case 1:
                    if (!PlayerProgress.Instance.GetGuideFirst())
                    {
                        PlayerProgress.Instance.SetGuideFirst();
                        yield return new WaitForSeconds(2.8f);
                        UIManager.Instance.newGuide1.SetActive(true);
                        yield return new WaitForSeconds(hideTime);
                        UIManager.Instance.newGuide1.SetActive(false);
                        UIManager.Instance.newGuide2.SetActive(true);
                        yield return new WaitForSeconds(hideTime);
                        UIManager.Instance.newGuide2.SetActive(false);
                    }
                    break;
                case 2:
                    if (!PlayerProgress.Instance.GetGuideSecond())
                    {
                        PlayerProgress.Instance.SetGuideSecond();
                        UIManager.Instance.newGuide3.SetActive(true);
                        yield return new WaitForSeconds(hideTime);
                        UIManager.Instance.newGuide3.SetActive(false);
                        UIManager.Instance.newGuide4.SetActive(true);
                        yield return new WaitForSeconds(hideTime);
                        UIManager.Instance.newGuide4.SetActive(false);
                    }
                    break;
                case 3:
                    if (!PlayerProgress.Instance.GetGuideThird())
                    {
                        PlayerProgress.Instance.SetGuideThird();
                        UIManager.Instance.newGuide5.SetActive(true);
                        yield return new WaitForSeconds(hideTime);
                        UIManager.Instance.newGuide5.SetActive(false);                       
                    }
                    break;
                case 4:
                    if (!PlayerProgress.Instance.GetGuideFourth())
                    {
                        PlayerProgress.Instance.SetGuideFourth();
                        UIManager.Instance.newGuide6.SetActive(true);
                        yield return new WaitForSeconds(hideTime);
                        UIManager.Instance.newGuide6.SetActive(false);
                    }
                    break;
                default:
                    break;
            }       
        }
    }

}
