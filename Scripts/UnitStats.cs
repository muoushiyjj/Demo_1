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
        
        if (transform.tag == "Monster")
        {
            
            if (!oneActCompelet)//如果一次行动没有完成，造成伤害
            {
                Damage();
            }
            StartCoroutine(Main.DelayFuc(SetHasActed, 3f));
        }
        else
        {
            SetHasActed();
        }
        if (hasActed == true)
        {
            Damage();
        }
        damage = 0;


    }

    public void Attack2()
    {
        
        damage = 50;
        print(transform.name + "hasattack");
        if (transform.tag == "Monster")
        {
            StartCoroutine(Main.DelayFuc(SetHasActed, 3f));//如果是怪物就延迟设置hasActed
        }
        else
        {
            SetHasActed();
        }

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
