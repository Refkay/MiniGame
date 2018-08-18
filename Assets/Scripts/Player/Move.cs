﻿using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;
using UnityEngine.UI;
using DG.Tweening;

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
    private float mMoveSpeed = 500.0f;

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

    public Camera mCamera;

    private Vector3 velocity = Vector3.one;

    void Start()
    {
        mRg2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 3);

    }

    private void OnEnable()
    {
        EasyTouch.On_TouchStart += On_TouchStart;
        EasyTouch.On_TouchDown += On_TouchDown;
        EasyTouch.On_TouchUp += On_TouchUp;
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
        if (((mDragTime > mDragTimeThreshold) || (mOffsetDistance > mOffsetThreshold)) && isMoveable)
        {
            mMoveDirection.Normalize();
            mRg2D.velocity = mMoveDirection * mMoveSpeed;
            mDragTime = 0;
            mOffsetDistance = 0;
            isMoveable = false;
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
        if (mMoveDirection.magnitude >= mMinoffset)
        {
            mRg2D.Sleep();
            mMoveDirection.Normalize();
            //mRg2d.AddForce(moveDirection * mMoveSpeed);
            mRg2D.velocity = mMoveDirection * mMoveSpeed;
            isMoveable = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
           
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Property")
        {
            Debug.Log("On Collision");
            Vector3 targetPosition = new Vector3(collision.transform.position.x, collision.transform.position.y, mCamera.transform.position.z);
            //mCamera.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, targetPosition, 1.5f);
            mCamera.transform.DOMove(targetPosition, 1.5f);
            //mCamera.transform.Translate(new Vector3(collision.transform.position.x, collision.transform.position.y, mCamera.transform.position.z));
            //Destroy(collision.gameObject);
            mRg2D.Sleep();

        }
    }

    /// <summary>
    /// 物体在摄像机内
    /// </summary>
    private void OnBecameVisible()
    {
        
    }

    /// <summary>
    /// 不在摄像机内触发改方法
    /// </summary>
    private void OnBecameInvisible()
    {
        mRg2D.Sleep();
        gameObject.transform.position = new Vector3(-3, -2, 0);
    }
}