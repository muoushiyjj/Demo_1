using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSpawner : MonoBehaviour
{
    
    public List<GameObject> herosList = new List<GameObject>();
    public List<GameObject> monstersList = new List<GameObject>();

    private GameObject[] heros;
    private GameObject[] monsters;

    public int herosCount;
    public int monstersCount;
    private int hasSpawnedUnitCount=1;

     Vector3 heroPosition1=new Vector3(-5f,3f,0);
     Vector3 heroPosition2=new Vector3(-5f,0,0);
     Vector3 heroPosition3=new Vector3(-5f,-3f,0);
     Vector3 monsterPosition1 = new Vector3(5f, 3f, 0);
     Vector3 monsterPosition2 = new Vector3(5f, 0, 0);
     Vector3 monsterPosition3 = new Vector3(5f, -3f, 0);



    public List<string> herosNames=new List<string>();
    public List<string> monstersNames=new List<string>();

    private int index=0;


    // Start is called before the first frame update
    private void Awake()
    {

        herosCount = PlayerPrefs.GetInt("HerosCount");
        switch (herosCount)
        {
            case 1:
                herosNames.Add(PlayerPrefs.GetString("HeroName1"));
                break;
            case 2:
                herosNames.Add(PlayerPrefs.GetString("HeroName1"));
                herosNames.Add(PlayerPrefs.GetString("HeroName2"));
                break;
            case 3:
                herosNames.Add(PlayerPrefs.GetString("HeroName1"));
                herosNames.Add(PlayerPrefs.GetString("HeroName2"));
                herosNames.Add(PlayerPrefs.GetString("HeroName3"));
                break;
        }


        monstersCount = PlayerPrefs.GetInt("MonstersCount");
        switch (monstersCount)
        {
            case 1:
                monstersNames.Add(PlayerPrefs.GetString("MonsterName1"));
                break;
            case 2:
                monstersNames.Add(PlayerPrefs.GetString("MonsterName1"));
                monstersNames.Add(PlayerPrefs.GetString("MonsterName2"));
                break;
            case 3:
                monstersNames.Add(PlayerPrefs.GetString("MonsterName1"));
                monstersNames.Add(PlayerPrefs.GetString("MonsterName2"));
                monstersNames.Add(PlayerPrefs.GetString("MonsterName3"));
                break;
        }
        



        while (hasSpawnedUnitCount <=herosCount)//计数生成heroprefab
        {
            if (index <= herosCount - 1)
            {
                GameObject obj = Resources.Load<GameObject>("Prefabs/" + herosNames[index]);//根据hero的名字加载hero
                obj = Instantiate(obj);
                herosList.Add(obj);
                switch (index)
                {
                    case 0:obj.transform.position = heroPosition2;
                        obj.name = "Hero1";
                        break;
                    case 1:obj.transform.position = heroPosition1;
                        obj.name= "Hero2";
                        break;
                    case 2:obj.transform.position = heroPosition3;
                        obj.name = "Hero3";
                        break;

                }//按顺序排位置
                index++;
            }
            hasSpawnedUnitCount++;
            //print("生成hero数量：" + (hasSpawnedUnitCount - 1));
            
        }
        hasSpawnedUnitCount = 1;//计数重置
        index = 0;
        while (hasSpawnedUnitCount <= monstersCount)//计数生成monsterprefab
        {
            if (index <= monstersCount - 1)
            {
                GameObject obj = Resources.Load<GameObject>("Prefabs/" + monstersNames[index]);//根据monster的名字加载monster
                obj = Instantiate(obj);
                monstersList.Add(obj);
                switch (index)
                {
                    case 0:
                        obj.transform.position = monsterPosition2;
                        obj.name = "Monster1";
                        break;
                    case 1:
                        obj.transform.position = monsterPosition1;
                        obj.name = "Monster2";
                        break;
                    case 2:
                        obj.transform.position = monsterPosition3;
                        obj.name = "Monster3";
                        break;

                }
                index++;
            }
            hasSpawnedUnitCount++;
            
        }
        index = 0;



    }
    void Start()
    {
        
        //heros = GameObject.FindGameObjectsWithTag("Hero");//初始化两个数组
        //monsters = GameObject.FindGameObjectsWithTag("Monster");
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
