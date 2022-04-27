using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManage : MonoBehaviour
{
    private GameObject[] heros;//����Ӣ�۵ļ���
    private GameObject[] monsters;//���й���ļ���
    private List<GameObject> hero = new List<GameObject>();
    private List<GameObject> monster = new List<GameObject>();
    private List<GameObject> team;//��ǰ�����Ķ���
    private GameObject currentUnit;
    public  GameObject attackedUnit;






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
                    if (team == hero)//�л�����
                    {
                        StartCoroutine(Main.DelayFuc(ChangeToMonster, 3f));
                        GetComponent<UIManager>().menuShow = false;//��ҹ�����ر�ui
                    }
                    else if (team == monster)//�л�����
                    {
                        ChangeToHero();
                    }

                }
            }
        }
       
    }//�ı����ķ���

    void ChangeToMonster()
    {
        team = monster;
        
    }
    void ChangeToHero()
    {
        team = hero;
        GetComponent<UIManager>().menuShow = true;//�����ж���ʹ�ui
    }
   
    private void MonsterAutoAct()
    {
        if (team == monster)//����ǹ���������Զ�����
        {
            Attack1();
            
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
        if (team == hero)
        {
            attackedUnit = monster[0];
        }
        else if (team == monster)
        {
            attackedUnit = hero[0];
        }
        currentUnit = team[index];
        team.Sort(new CompareUnitsSpeed());//ʵ���˰�unitstats.speed��������
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
