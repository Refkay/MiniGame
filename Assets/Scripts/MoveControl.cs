using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControl : MonoBehaviour
{


    [SerializeField]
    private ETCJoystick joystick;//虚拟摇杆
    //[SerializeField]
 

    [SerializeField]
    private float moveSpeed = 100.0f;//移动速度
    [SerializeField]
    private float rotSpeed = 60;//移动速度
    [SerializeField]
    private Rigidbody2D rb2d;
    [SerializeField]
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //joystick.onMoveEnd.AddListener(() => onMoveEnd());

        ////方式一：按键方法注册
        //joystick.OnPressLeft.AddListener(() => JoystickHandlerMoving());
        //joystick.OnPressRight.AddListener(() => JoystickHandlerMoving());
        //joystick.OnPressUp.AddListener(() => JoystickHandlerMoving());
        //joystick.OnPressDown.AddListener(() => JoystickHandlerMoving());

    }

    //方式二：输入监测
    void FixedUpdate()
    {
        if (ETCInput.GetAxisPressedUp("Vertical"))
        {
            JoystickHandlerMoving();
        }


        if (ETCInput.GetAxisPressedDown("Vertical"))
        {
            JoystickHandlerMoving();
        }


        if (ETCInput.GetAxisPressedLeft("Horizontal"))
        {
            JoystickHandlerMoving();

        }
        if (ETCInput.GetAxisPressedRight("Horizontal"))
        {
            JoystickHandlerMoving();

        }


    }




    void JoystickHandlerMoving()
    {
        //if (joystick.name != "New Joystick")
        //{
        //    return;
        //}

        //获取虚拟摇杆偏移量  
        float h = joystick.axisX.axisValue;
        float v = joystick.axisY.axisValue;

        Vector3 moveDirection = new Vector3(h, v, 0);
        moveDirection.Normalize();
        float step = rotSpeed * Time.fixedDeltaTime;
        if (Mathf.Abs(h) > 0.05f || (Mathf.Abs(v) > 0.05f))
        {              
            rb2d.AddForce(moveDirection * moveSpeed);           
        }
    }


}