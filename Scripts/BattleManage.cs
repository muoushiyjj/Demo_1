using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManage : MonoBehaviour
{

    private GameObject[] heros;//所有英雄的集合
    private GameObject[] monsters;//所有怪物的集合
    public List<GameObject> hero = new List<GameObject>();
    public List<GameObject> monster = new List<GameObject>();
    private List<GameObject> team;//当前操作的队伍
    private GameObject currentUnit;
    
    public  GameObject attackedUnit;

    public GameObject choseMonsterCanvas;
    private bool hasChosedMonster;

    private delegate void AttackFunction();
    AttackFunction attackFunction;

    public bool monsterNeedAim;



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
                monsterNeedAim = true;//一个怪物攻击完再选新的敌人
                

                if (index < team.Count - 1)
                {
                    index += 1;
                    if (team == hero)
                    {
                        GetComponent<UIManager>().menuShow = true;//一个hero行动后如果还有hero没行动就打开菜单
                    }

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
                        StartCoroutine(Main.DelayFuc(ChangeToMonster, 3f));//hero行动完
                        monsterNeedAim = true;
                        
                    }
                    else if (team == monster)//切换队伍
                    {
                        ChangeToHero();
                        GetComponent<UIManager>().menuShow = true;//怪物攻击完打开ui
                    }

                }
            }
        }
       
    }//改变队伍的方法
    private void SetUIMenu()//关闭行动菜单的方法
    {
        GetComponent<UIManager>().menuShow = false;
    }
    void ChangeToMonster()
    {
        team = monster;
        
    }
    void ChangeToHero()
    {
        team = hero;
        GetComponent<UIManager>().menuShow = true;//怪物行动完就打开ui
    }
   
    void SomeUnitDead()
    {
        foreach(GameObject obj in heros)
        {
            if (obj.GetComponent<UnitStats>().isDead)
            {
                
                int i = hero.IndexOf(obj);//把单位在表中的索引记录下来
                hero.Remove(obj);
                int tempCount = GetComponent<ShowBar>().heroBars.Count / 2;
                Slider tempHP = GetComponent<ShowBar>().heroBars[i];
                Slider tempMP = GetComponent<ShowBar>().heroBars[tempCount + i];

                GetComponent<ShowBar>().heroBars.RemoveAt(tempCount + i);
                GetComponent<ShowBar>().heroBars.RemoveAt(i);

                Destroy(tempHP);
                Destroy(tempMP);
                Destroy(obj);
            }
        }
        foreach(GameObject obj in monsters)
        {
            if (obj.GetComponent<UnitStats>().isDead)
            {
                int i = monster.IndexOf(obj);
                monster.Remove(obj);
                Slider tempHP = GetComponent<ShowBar>().monsterBars[i];
                Button tempButton = GetComponent<ChoseMonster>().choseMonsterButtonsList[i];

                GetComponent<ShowBar>().monsterBars.RemoveAt(i);

                GetComponent<ChoseMonster>().choseMonsterButtonsList.RemoveAt(i);
                foreach(Button b in GetComponent<ChoseMonster>().choseMonsterButtonsList)
                {
                    b.onClick.RemoveAllListeners();//删掉所有的点击事件
                }
                GetComponent<ChoseMonster>().SetChoseButtonOnClick();//重新设置点击事件


                Destroy(tempHP);
                Destroy(tempButton);
                Destroy(obj);

            }
        }
    }
    private void MonsterAutoAct()
    {
        if (team == monster)//如果是怪物操作就自动攻击
        {

            MonsterAttack();
            
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
        if (team == monster&&monsterNeedAim)//要不然会一直选，攻击的和伤害到的不是同一个人
        {
            int index = (int)Mathf.Floor(Random.Range(0f, hero.Count));
            attackedUnit = hero[index];
            monsterNeedAim = false;
        }
        currentUnit = team[index];
        team.Sort(new CompareUnitsSpeed());//实现了按unitstats.speed排列数组
        //print(currentUnit.GetComponent<UnitStats>().hasActed);

        heros = GameObject.FindGameObjectsWithTag("Hero");//更新heros
        monsters = GameObject.FindGameObjectsWithTag("Monster");//更新monsters

        SomeUnitDead();





    }

    public void ChoseMonster(int index)//设置被攻击对象,并攻击
    {
        
        attackedUnit = monster[index];
        hasChosedMonster = true;
        if (hasChosedMonster)
        {
            attackFunction();//攻击委托
        }
        hasChosedMonster = false;
        choseMonsterCanvas.SetActive(false);//关闭选择画布
    }


    public void Attack1()
    {
        choseMonsterCanvas.SetActive(true);//打开选择画布
        attackFunction =currentUnit.GetComponent<UnitStats>().Attack1;//设置攻击委托
        SetUIMenu();
        

    }
    public void Attack2()
    {
        
        currentUnit.GetComponent<UnitStats>().Attack2();
        

    }

    private void MonsterAttack()
    {
        currentUnit.GetComponent<UnitStats>().MonsterAttack();
    }

}
