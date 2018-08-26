using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

namespace MiniGame
{
    /// <summary>
    /// 绘制星座图的类
    /// </summary>
    public class DrawStarTrack : MonoBehaviour
    {
        public GameObject bgImage;
        private List<Vector3> transformList = new List<Vector3>();      
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
            //这里加载Star的位置信息，这里先写死
            Vector3 star1 = new Vector3(-0.93f, 1.97f, 0);
            Vector3 star2 = new Vector3(0.67f, 0.66f, 0);
            Vector3 star3 = new Vector3(2, -0.66f,0);
            Vector3 star4 = new Vector3(1.08f, -2.0f, 0);
            transformList.Add(star1);
            transformList.Add(star2);
            transformList.Add(star3);
            transformList.Add(star4);
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
            starPosition = transformList[0];
            for (int i = 1; i < transformList.Count; i ++)
            {
                targetPosition = transformList[i];
                GameObject go = GameObject.Instantiate(Resources.Load("Prefabs/LineEff")) as GameObject;
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
            yield return new WaitForSeconds(2.0f);
            //进入下一个关卡
            GameManagers.mMissionManager.GoToNextLevel();
            yield return null;
        }      
    }

}
