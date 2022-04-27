using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManage : MonoBehaviour
{
    private GameObject[] heros;//所有英雄的集合
    private GameObject[] monsters;//所有怪物的集合
    private List<GameObject> hero = new List<GameObject>();
    private List<GameObject> monster = new List<GameObject>();
    private List<GameObject> team;//当前操作的队伍
    private GameObject currentUnit;
    public  GameObject attackedUnit;






    private int index;


    private void Start()
    {
        index = 0;//索引初始化为0
        heros = GameObject.FindGameObjectsWithTag("Hero");//初始化heros集合//根据tag去找所有元素
        monsters = GameObject.FindGameObjectsWithTag("Monster");//初始化monster集合

        foreach (GameObject obj in heros)
        {
            hero.Add(obj);//把heros里每一个hero都添加到hero列表中
            hero.Sort(new CompareUnitsSpeed());//实现了按unitstats.speed排列数组
        }
        foreach (GameObject obj in monsters)
        {
            monster.Add(obj);
            monster.Sort(new CompareUnitsSpeed());//实现了按unitstats.speed排列数组
        }
        team = hero;//玩家先行动
        
    }

    private void Awake()
    {

    }//初始化一些设置

    void ChangeTeam()
    {
        if (index <= team.Count - 1)//防止索引超过集合的数量
        {
            if (team[index].GetComponent<UnitStats>().hasActed)//如果当前的单位行动完，执行下面
            {

                if (index < team.Count - 1)
                {
                    index += 1;

                }
                else if (index == team.Count - 1)
                {
                    index = 0;
                    
                    foreach (GameObject obj in hero)
                    {
                        obj.GetComponent<UnitStats>().hasActed = false;
                    }
                    foreach(GameObject obj in monster)
                    {
                        obj.GetComponent<UnitStats>().hasActed = false;
                        obj.GetComponent<UnitStats>().oneActCompelet = false;

                    }
                    if (team == hero)//切换队伍
                    {
                        StartCoroutine(Main.DelayFuc(ChangeToMonster, 3f));
                        GetComponent<UIManager>().menuShow = false;//玩家攻击完关闭ui
                    }
                    else if (team == monster)//切换队伍
                    {
                        ChangeToHero();
                    }

                }
            }
        }
       
    }//改变队伍的方法

    void ChangeToMonster()
    {
        team = monster;
        
    }
    void ChangeToHero()
    {
        team = hero;
        GetComponent<UIManager>().menuShow = true;//怪物行动完就打开ui
    }
   
    private void MonsterAutoAct()
    {
        if (team == monster)//如果是怪物操作就自动攻击
        {
            Attack1();
            
        }
    }
    // Update is called once per frame
    private void LateUpdate()
    {
        
        ChangeTeam();
        if (!currentUnit.GetComponent<UnitStats>().oneActCompelet)//当一个怪物没有进行过行动执行自动行动
        {
            MonsterAutoAct();
            currentUnit.GetComponent<UnitStats>().oneActCompelet = true;
        }


    }
    void Update()
    {
        if (team == hero)
        {
            attackedUnit = monster[0];
        }
        else if (team == monster)
        {
            attackedUnit = hero[0];
        }
        currentUnit = team[index];
        team.Sort(new CompareUnitsSpeed());//实现了按unitstats.speed排列数组
        print(currentUnit.GetComponent<UnitStats>().hasActed);

    }


    public void Attack1()
    {
       
        currentUnit.GetComponent<UnitStats>().Attack1();
        

    }
    public void Attack2()
    {
        
        currentUnit.GetComponent<UnitStats>().Attack2();
        

    }

}
