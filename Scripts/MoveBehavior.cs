using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehavior : MonoBehaviour
{

    public float speedMutiplier=1f;

    private Animator ani;
    private Rigidbody2D rb;
    private Vector2 velocity;



    //private void OnCollisionEnter2D(Collision collision)
    //{
        
    //}
    void SetAnimator()//设置动画控制器中的变量
    {
        ani.SetInteger("VelocityX", (int)velocity.x);
        ani.SetInteger("VelocityY", (int)velocity.y);
        //print("X:"+ani.GetInteger("VelocityX"));
        //print("Y:"+ani.GetInteger("VelocityY"));
    }
    void SetVelocity()
    {
        velocity.x = Input.GetAxisRaw("Horizontal")*speedMutiplier;//设置速度的x值
        velocity.y = Input.GetAxisRaw("Vertical")*speedMutiplier;//设置速度的y值
        //sprint(velocity.x);
        //print(velocity.y);
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();//获取对象身上的2d刚体
        ani = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = velocity;//设置刚体的速度
        //print(rb.velocity);
        SetVelocity();
        SetAnimator();
    }
}
