using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
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
    private float velocity;


    private void WalkToMonster()
    {

    }
    public void Damage()
    {
        GameObject.Find("BattleManager").GetComponent<BattleManage>().attackedUnit.GetComponent<UnitStats>().HP -= damage;
    }
    void SetHasActed()
    {
        hasActed = true;
    } 
    // Start is called before the first frame update
    public void Attack1()
    {

        damage = 20;
        Damage();



        SetHasActed();//�ж����
        damage = 0;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        HPValue = HP / maxHP;
        MPValue = MP / maxMP;
        
    }
}
