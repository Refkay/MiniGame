﻿using UnityEngine;
using System.Collections;
using MiniGameComm;
using DG.Tweening;
using System.Collections.Generic;

namespace MiniGame
{
    /// <summary>
    /// 导弹的操作，吃到导弹随机炸掉一块石头
    /// </summary>
    public class BulletReatcion : MonoBehaviour
    {
        public GameObject UFO;
        public ParticleSystem mParticleSystemEffect;
        private Vector3 mInitialPosition;
        private Color initinalStoneColor;
        private bool canReact = true;
        private bool isAniComplete = true;
        private void Awake()
        {
            MessageBus.Register<OnSubLevelFailedMsg>(OnSubLevelFailed);
            mInitialPosition = gameObject.transform.position;
            initinalStoneColor = gameObject.GetComponent<SpriteRenderer>().color;
        }

        private void OnDestroy()
        {
            MessageBus.UnRegister<OnSubLevelFailedMsg>(OnSubLevelFailed);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player" && canReact)
            {
                //GameObject stone = StoneManager.Instance.GetRadomStoneTrasform();
                //SetPlayerAngle(stone.transform.position);
                //gameObject.transform.DOMove(stone.transform.position, 0.5f).OnComplete(() => {
                //    stone.gameObject.SetActive(false);
                //    gameObject.SetActive(false);
                //    //这里要播放炸掉石头的特效
                //});
                canReact = false;
                mParticleSystemEffect.Play();
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                gameObject.transform.FindChild("Particle System").gameObject.SetActive(false);
                if (isAniComplete)
                {
                    canReact = false;
                    StartCoroutine(FlyStoneAndFade());
                }
            }
        }

        private bool OnSubLevelFailed(OnSubLevelFailedMsg msg)
        {        
            gameObject.transform.position = mInitialPosition;
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);         
            //gameObject.SetActive(true);
            //gameObject.GetComponent<SpriteRenderer>().color = initinalStoneColor;
            //gameObject.transform.FindChild("Particle System").gameObject.SetActive(true);
            return false;
        }

        //导弹朝向某个角度
        private void SetPlayerAngle(Vector3 targetPosition)
        {
            float angle;
            Vector2 targetDir = targetPosition - gameObject.transform.position;
            angle = Vector2.Angle(targetDir, Vector3.up);
            if (targetPosition.x > gameObject.transform.position.x)
            {
                angle = -angle;
            }
            this.transform.eulerAngles = new Vector3(0, 0, angle);
        }

        private IEnumerator FlyStoneAndFade()
        {
            isAniComplete = false;
            //这里播放特效
            UFO.SetActive(true);
            //播放音乐
            AudioManager.Instance.PlayOneShotIndex(4);
            Animator animator = UFO.GetComponent<Animator>();
            animator.enabled = true;
            animator.Play("UFOAni", -1, 0.0f);                       
            yield return new WaitForSeconds(1.5f);

            List<GameObject> stoneList = StoneManager.Instance.GetRadomStoneTrasform();
            for (int i = 0; i < stoneList.Count; i ++)
            {
                GameObject stone = stoneList[i];
                Vector3 initinalStonePosition = stone.gameObject.transform.position;
                Color initinalStoneColor = stone.GetComponent<SpriteRenderer>().color;
                Vector3 targetPosition = MissionData.GetCameraPosition(MissionManager.Instance.mCurLevel,
                    MissionManager.Instance.mCurSubLevel);

                targetPosition.y += 6.5f;

                //stone.GetComponent<SpriteRenderer>().DOColor(new Color(0, 0, 0, 0), 1.0f);
                //石头要变成Trigger，不然在飞行的过程中撞到Player会导致Player死亡
                stone.gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
                stone.gameObject.transform.DOMove(targetPosition, 1.0f).OnComplete(() =>
                {
                    stone.gameObject.SetActive(false);
                    stone.gameObject.GetComponent<PolygonCollider2D>().isTrigger = false;
                    stone.gameObject.transform.position = initinalStonePosition;
                    stone.GetComponent<SpriteRenderer>().color = initinalStoneColor;
                    //这里要播放炸掉石头的特效
                });
            }
           
            yield return new WaitForSeconds(2.5f);
            isAniComplete = true;
            animator.enabled = false;
            UFO.SetActive(false);          
        }
    }
}

