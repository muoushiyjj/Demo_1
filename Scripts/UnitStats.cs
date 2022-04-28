using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    private Animator ani;

    public float HP=100;
    public float maxHP=100;
    public float MP=100;
    public float maxMP=100;
    public float speed=5;
    public float attack=10;
    public float magic=50;
    public float HPValue;
    public float MPValue;
    public float damage;
    


    public bool hasActed;
    public bool canDamaged;
    public bool hasDamaged;
    public bool oneActCompelet;
    public bool isDead;


    private Rigidbody2D rb;
    private float velocity=3f;//ս�������е���·�ٶ�
    private Vector3 direction;
    private GameObject attackedUnit;
    private Vector3 originalPosition;
    private float xoffset=0.6f;//��ʾҪ�ߵ�������ǰ����
    private Vector3 aim;
    private bool reachGoal;


    private void ResetAnimatorBool()
    {
        ani.SetBool("CanAttack", false);
        ani.SetBool("CanIdle", false);
        ani.SetBool("CanMoveToAim", false);
    }

    private void CalculateTwoVector3close(Vector3 currentPositon,Vector3 aim)//�����Լ�λ�ú�Ŀ�������
    {
        Vector3 tempVector;
        tempVector = aim - currentPositon;
        if (tempVector.magnitude < 0.2)
        {
            reachGoal = true;
        }
    }
    private void WalkToAim(Vector3 aim,Vector3 currentPosition)
    {
        velocity = 5f;
        direction = (aim - currentPosition).normalized;
        rb.velocity = direction * velocity;
        
    }
    public void Damage()
    {
        attackedUnit = GameObject.Find("BattleManager").GetComponent<BattleManage>().attackedUnit;
        attackedUnit.GetComponent<UnitStats>().HP -= damage;
        
    }

    void SetHasActed()
    {
        hasActed = true;
    } 
    // Start is called before the first frame update
    private void MoveBack()
    {
        
        aim = originalPosition;
        WalkToAim(aim, transform.position);

    }
    private void ResetDamage()
    {
        damage = 0;
    }
    private void FindAimPosition()//�ҵ�Ŀ��λ��
    {
        attackedUnit = GameObject.Find("BattleManager").GetComponent<BattleManage>().attackedUnit;
        if (transform.tag == "Hero")
        {
            aim = attackedUnit.transform.position - new Vector3(xoffset, 0, 0);
        }
        else if (transform.tag == "Monster")
        {
            aim = attackedUnit.transform.position + new Vector3(xoffset, 0, 0);
        }
    }
    public void Attack1()
    {
        ResetAnimatorBool();
        originalPosition = transform.position;//��¼ԭʼλ��
        damage = 50;//�˺��ڶ��������
        FindAimPosition();
        WalkToAim(aim,transform.position);
        ani.SetBool("CanMoveToAim", true);
    }
    public void Attack2()
    {

    }

    public void MonsterAttack()
    {
        ResetAnimatorBool();
        originalPosition = transform.position;
        damage = 30;//�˺��ڶ��������
        FindAimPosition();
        WalkToAim(aim, transform.position);
        ani.SetBool("CanMoveToAim", true);

        //StartCoroutine(Main.DelayFuc(SetHasActed, 3f));//�ӳ������ж����
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    private void IfReachGoal()
    {
        if (reachGoal)//����Ŀ����ͣ��
        {
            rb.velocity = Vector3.zero;//�ٶ���Ϊ0
            ani.SetBool("CanAttack", true);
            if (aim == originalPosition)//�ص�ԭ����λ��
            {
                transform.position = originalPosition;
                ani.SetBool("CanIdle", true);
                ani.SetBool("CanAttack", false);
                ani.SetBool("CanMoveToAim", false);

                SetHasActed();
                ResetDamage();

                
                
                
            }
            aim = Vector3.zero;
        }
        reachGoal = false;

    }

    // Update is called once per frame
    void Update()
    {
        HPValue = HP / maxHP;
        MPValue = MP / maxMP;
        CalculateTwoVector3close(transform.position, aim);
        IfReachGoal();
        if (HP <= 0)
        {
            isDead = true;
        }
        


    }
}
