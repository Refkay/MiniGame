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
        public GameObject bgImage;
        public Camera mainCamera;
        private Vector3 starPosition;
        private Vector3 targetPosition;
      
        //绘制线的时间
        private float lineTime = 2.0f;
        //绘制线的速度
        private float lineSpeed = 2.0f;
        //判断能否绘制下一个点
        private bool isDrawAbl;

        private Color showColor = new Color(1, 1, 1, 1);
        private Color hideColor = new Color(0, 0, 0, 0);

        void Start()
        {        
            DrawAllStarTrack();      
        }

        // Update is called once per frame
        void Update()
        {

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
            starPosition = positions[0].position;
            for (int i = 1; i < positions.Length; i ++)
            {
                targetPosition = positions[i].position;
                GameObject go = GameObject.Instantiate(Resources.Load("Prefabs/LineEff")) as GameObject;
                go.transform.parent = gameObject.transform;
                go.transform.position = starPosition;               
                TrailAutoMoveComp t = go.AddComponent<TrailAutoMoveComp>();
                t.Init(targetPosition, lineTime);
                starPosition = targetPosition;
                yield return new WaitForSeconds(lineTime);
            }

            if (bgImage != null)
            {
                bgImage.SetActive(true);
            }
            
            yield return new WaitForSeconds(4.0f);

            mainCamera.DOOrthoSize(1.0f, 4.0f);
            yield return new WaitForSeconds(3.0f);
            //进入下一个关卡
            GameManagers.mMissionManager.GoToNextLevel();
            yield return null;
        }
     
    }

}
