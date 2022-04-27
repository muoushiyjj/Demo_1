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
    public float attack=20;
    public float magic=50;
    public float HPValue;
    public float MPValue;
    public float damage;
    


    public bool hasActed;
    public bool canDamaged;
    public bool hasDamaged;
    public bool oneActCompelet;

    private Rigidbody2D rb;
    private float velocity=3f;//ս�������е���·�ٶ�
    private Vector3 direction;
    private GameObject attackedUnit;
    private Vector3 originalPosition;
    private float xoffset=0.6f;//��ʾҪ�ߵ�������ǰ����
    private Vector3 aim;
    private bool reachgoal;


    private void ResetAnimatorBool()
    {
        ani.SetBool("CanAttack", false);
        ani.SetBool("CanIdle", false);
        ani.SetBool("CanMoveToMonster", false);
    }

    private void CalculateTwoVector3close(Vector3 currentPositon,Vector3 aim)//�����Լ�λ�ú�Ŀ�������
    {
        Vector3 tempVector;
        tempVector = aim - currentPositon;
        if (tempVector.magnitude < 0.2)
        {
            reachgoal = true;
        }
    }
    private void WalkToAim(Vector3 aim,Vector3 currentPosition)
    {
        velocity = 3f;
        direction = (aim - currentPosition).normalized;
        rb.velocity = direction * velocity;
        
    }
    public void Damage()
    {
        attackedUnit = GameObject.Find("BattleManager").GetComponent<BattleManage>().attackedUnit;
        attackedUnit.GetComponent<UnitStats>().HP -= damage;
        print("����˺���");
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
    public void Attack1()
    {
        originalPosition = transform.position;//��¼ԭʼλ��
        damage = 20;
        attackedUnit = GameObject.Find("BattleManager").GetComponent<BattleManage>().attackedUnit;
        aim = attackedUnit.transform.position - new Vector3(xoffset, 0, 0);
        WalkToAim(aim,transform.position);
        ani.SetBool("CanMoveToMonster", true);



        //�ж���ɻص�ԭλ������hasacted

    }
    public void Attack2()
    {

    }

    public void MonsterAttack()
    {
        damage = 10;
        if (!oneActCompelet)
        {
            Damage();
        }
        damage = 0;
        StartCoroutine(Main.DelayFuc(SetHasActed, 3f));//�ӳ������ж����
    }

    void Start()
    {
        if (transform.tag == "Hero")
        {
            rb = GetComponent<Rigidbody2D>();
            ani = GetComponent<Animator>();
        }
        
    }



    // Update is called once per frame
    void Update()
    {
        HPValue = HP / maxHP;
        MPValue = MP / maxMP;
        CalculateTwoVector3close(transform.position, aim);

        if (reachgoal)//����Ŀ����ͣ��
        {
            rb.velocity = Vector3.zero;//�ٶ���Ϊ0
            ani.SetBool("CanAttack", true);
            if (aim == originalPosition)//�ص�ԭ����λ��
            {
                transform.position = originalPosition;
                SetHasActed();
                ResetDamage();
                ResetAnimatorBool();
                ani.SetBool("CanIdle", true);
            }
            aim = Vector3.zero;
        }
        reachgoal = false;
        

        if (transform.tag == "Hero")
        {
            print("aim:" + aim);
        }
        


    }
}
