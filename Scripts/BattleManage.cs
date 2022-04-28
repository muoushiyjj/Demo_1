using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManage : MonoBehaviour
{

    private GameObject[] heros;//����Ӣ�۵ļ���
    private GameObject[] monsters;//���й���ļ���
    public List<GameObject> hero = new List<GameObject>();
    public List<GameObject> monster = new List<GameObject>();
    private List<GameObject> team;//��ǰ�����Ķ���
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
        index = 0;//������ʼ��Ϊ0
        heros = GameObject.FindGameObjectsWithTag("Hero");//��ʼ��heros����//����tagȥ������Ԫ��
        monsters = GameObject.FindGameObjectsWithTag("Monster");//��ʼ��monster����

        foreach (GameObject obj in heros)
        {
            hero.Add(obj);//��heros��ÿһ��hero����ӵ�hero�б���
            hero.Sort(new CompareUnitsSpeed());//ʵ���˰�unitstats.speed��������
        }
        foreach (GameObject obj in monsters)
        {
            monster.Add(obj);
            monster.Sort(new CompareUnitsSpeed());//ʵ���˰�unitstats.speed��������
        }
        team = hero;//������ж�
        
    }

    private void Awake()
    {

    }//��ʼ��һЩ����

    void ChangeTeam()
    {
        if (index <= team.Count - 1)//��ֹ�����������ϵ�����
        {
            if (team[index].GetComponent<UnitStats>().hasActed)//�����ǰ�ĵ�λ�ж��ִ꣬������
            {
                monsterNeedAim = true;//һ�����﹥������ѡ�µĵ���
                

                if (index < team.Count - 1)
                {
                    index += 1;
                    if (team == hero)
                    {
                        GetComponent<UIManager>().menuShow = true;//һ��hero�ж����������heroû�ж��ʹ򿪲˵�
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
                    if (team == hero)//�л�����
                    {
                        StartCoroutine(Main.DelayFuc(ChangeToMonster, 3f));//hero�ж���
                        monsterNeedAim = true;
                        
                    }
                    else if (team == monster)//�л�����
                    {
                        ChangeToHero();
                        GetComponent<UIManager>().menuShow = true;//���﹥�����ui
                    }

                }
            }
        }
       
    }//�ı����ķ���
    private void SetUIMenu()//�ر��ж��˵��ķ���
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
        GetComponent<UIManager>().menuShow = true;//�����ж���ʹ�ui
    }
   
    void SomeUnitDead()
    {
        foreach(GameObject obj in heros)
        {
            if (obj.GetComponent<UnitStats>().isDead)
            {
                
                int i = hero.IndexOf(obj);//�ѵ�λ�ڱ��е�������¼����
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
                    b.onClick.RemoveAllListeners();//ɾ�����еĵ���¼�
                }
                GetComponent<ChoseMonster>().SetChoseButtonOnClick();//�������õ���¼�


                Destroy(tempHP);
                Destroy(tempButton);
                Destroy(obj);

            }
        }
    }
    private void MonsterAutoAct()
    {
        if (team == monster)//����ǹ���������Զ�����
        {

            MonsterAttack();
            
        }
    }
    // Update is called once per frame
    private void LateUpdate()
    {
        
        ChangeTeam();
        if (!currentUnit.GetComponent<UnitStats>().oneActCompelet)//��һ������û�н��й��ж�ִ���Զ��ж�
        {
            MonsterAutoAct();
            currentUnit.GetComponent<UnitStats>().oneActCompelet = true;
        }
        
        


    }
    void Update()
    {
        if (team == monster&&monsterNeedAim)//Ҫ��Ȼ��һֱѡ�������ĺ��˺����Ĳ���ͬһ����
        {
            int index = (int)Mathf.Floor(Random.Range(0f, hero.Count));
            attackedUnit = hero[index];
            monsterNeedAim = false;
        }
        currentUnit = team[index];
        team.Sort(new CompareUnitsSpeed());//ʵ���˰�unitstats.speed��������
        //print(currentUnit.GetComponent<UnitStats>().hasActed);

        heros = GameObject.FindGameObjectsWithTag("Hero");//����heros
        monsters = GameObject.FindGameObjectsWithTag("Monster");//����monsters

        SomeUnitDead();





    }

    public void ChoseMonster(int index)//���ñ���������,������
    {
        
        attackedUnit = monster[index];
        hasChosedMonster = true;
        if (hasChosedMonster)
        {
            attackFunction();//����ί��
        }
        hasChosedMonster = false;
        choseMonsterCanvas.SetActive(false);//�ر�ѡ�񻭲�
    }


    public void Attack1()
    {
        choseMonsterCanvas.SetActive(true);//��ѡ�񻭲�
        attackFunction =currentUnit.GetComponent<UnitStats>().Attack1;//���ù���ί��
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
