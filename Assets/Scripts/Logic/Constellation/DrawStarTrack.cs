using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using DG.Tweening;

namespace MiniGame
{
    /// <summary>
    /// 绘制星座图的类
    /// </summary>
    public class DrawStarTrack : MonoBehaviour
    {
        public Transform[] positions;
        public GameObject cameraBg;
        public GameObject bgImage;
        public Camera mainCamera;
        private Vector3 starPosition;
        private Vector3 targetPosition;
        //绘制线的时间
        private float lineTime = 2.6f;
        //绘制线的速度
        private float lineSpeed = 2.0f;
        //判断能否绘制下一个点
        private bool isDrawAbl;

        public Image _image1;
        public Text _text1;
        public Text _text2;

        private int _shotIndex = 1;
        //控制播放完才可以点
        private bool _listenFlag;

        private Color _showColor = new Color(1, 1, 1, 1);
        private Color _hideColor = new Color(0, 0, 0, 0);

        private void SceneReset()
        {
            _text1.color = _hideColor;
            _text2.color = _hideColor;          
        }

        private void HideCanvas()
        {
            _image1.gameObject.SetActive(false);
            _text1.gameObject.SetActive(false);
            _text2.gameObject.SetActive(false);          
        }

        void Start()
        {
            _listenFlag = true;
            SceneReset();
            Shot1();
        }

        private void Shot1()
        {
            _listenFlag = false;
            SceneReset();
            _text1.DOColor(_showColor, 2.0f).OnComplete(_resetFlag);
        }

        private void Shot2()
        {
            _listenFlag = false;
            _text1.DOColor(_hideColor, 1.0f);
            _text2.DOColor(_showColor, 2.0f).OnComplete(_resetFlag);
        }

        private void _resetFlag()
        {
            _listenFlag = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0) && _listenFlag)
            {
                if (_shotIndex == 0)
                {
                    //Shot1();
                }
                else if (_shotIndex == 1)
                {
                    Shot2();
                }
                else if (_shotIndex == 2)
                {
                    HideCanvas();
                    DrawAllStarTrack();
                }
                _shotIndex++;
            }
        }

        /// <summary>
        /// 绘制所有路线
        /// </summary>
        public void DrawAllStarTrack()
        {
            StartCoroutine(DrawAllLine());         
        } 

        IEnumerator DrawAllLine()
        {
            yield return new WaitForSeconds(0.3f);
            starPosition = positions[0].position;
            for (int i = 1; i < positions.Length; i ++)
            {
                targetPosition = positions[i].position;
                GameObject go = GameObject.Instantiate(Resources.Load("Prefabs/LineEff")) as GameObject;
                go.transform.parent = gameObject.transform;
                go.transform.position = starPosition;               
                TrailAutoMoveComp t = go.AddComponent<TrailAutoMoveComp>();
                t.Init(targetPosition, lineTime);          
                Vector3 nextCameraPosition = new Vector3(targetPosition.x, targetPosition.y, -10);
                mainCamera.transform.DOMove(nextCameraPosition, lineTime);
                starPosition = targetPosition;
                yield return new WaitForSeconds(lineTime - 0.1f);
            }        
            bgImage.gameObject.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 1), 5.5f);
            mainCamera.transform.DOMove(new Vector3(0,0, -10), 4.0f);
            mainCamera.DOOrthoSize(5.0f, 4.0f);
            cameraBg.gameObject.transform.DOScale(new Vector3(1, 1, 0), 4.0f);
            yield return new WaitForSeconds(4.0f);

            mainCamera.DOOrthoSize(1.0f, 4.0f);       
            yield return new WaitForSeconds(3.0f);
            //进入下一个关卡
            GameManagers.mMissionManager.GoToNextLevel();
            yield return null;
        }    
    }

}
