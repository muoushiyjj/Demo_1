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
    void SetAnimator()//���ö����������еı���
    {
        ani.SetInteger("VelocityX", (int)velocity.x);
        ani.SetInteger("VelocityY", (int)velocity.y);
        //print("X:"+ani.GetInteger("VelocityX"));
        //print("Y:"+ani.GetInteger("VelocityY"));
    }
    void SetVelocity()
    {
        velocity.x = Input.GetAxisRaw("Horizontal")*speedMutiplier;//�����ٶȵ�xֵ
        velocity.y = Input.GetAxisRaw("Vertical")*speedMutiplier;//�����ٶȵ�yֵ
        //sprint(velocity.x);
        //print(velocity.y);
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();//��ȡ�������ϵ�2d����
        ani = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = velocity;//���ø�����ٶ�
        //print(rb.velocity);
        SetVelocity();
        SetAnimator();
    }
}
