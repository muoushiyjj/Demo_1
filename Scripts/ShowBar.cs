using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowBar : MonoBehaviour
{
    private int herosCout;
    private int monstersCount;
    private int index;

    public float xoffset;
    public float HPyoffset;
    public float MPyoffset;

    private Slider tempBar;

    private List<Slider> heroBars=new List<Slider>();
    private List<Slider> monsterBars = new List<Slider>();
    private List<GameObject> herosList = new List<GameObject>();
    private List<GameObject> monstersList = new List<GameObject>();


    Vector2 Unit2DPosition;


    
    private int hasSpawnedBarCount=1;
    void SetUIPositionWithUnit()
    {
        while (index <= herosCout-1)//����hero��bar
        {
            heroBars[index].transform.position = herosList[index].transform.position + new Vector3(xoffset, HPyoffset,0);
            heroBars[herosCout+index].transform.position = herosList[index].transform.position + new Vector3(xoffset, MPyoffset, 0);
            index++;
        }
        index = 0;
        while (index <= monstersCount - 1)//����monster��bar
        {
            monsterBars[index].transform.position = monstersList[index].transform.position + new Vector3(xoffset, HPyoffset, 0);
            index++;
        }
        index = 0;
        

        //hero2DPosition = heroPrefabs[index].transform.position;
        //rts[index].position = hero2DPosition + new Vector2(xoffset, yoffset);
    }

    void SetSliderValueWithUnit()
    {
        while (index <= herosCout - 1)//����hero��bar
        {
            heroBars[index].value = herosList[index].GetComponent<UnitStats>().HPValue;
            heroBars[herosCout + index].value = herosList[index].GetComponent<UnitStats>().MPValue;
            index++;
            
            
        }
        index = 0;
        while (index <= monstersCount - 1)//����monster��bar
        {
            monsterBars[index].value = monstersList[index].GetComponent<UnitStats>().HPValue;
            index++;
        }
        index = 0;
    }

    private void Awake()
    {
        index = 0;
        //SpawnAllBar();
        herosCout = this.GetComponent<BattleSpawner>().herosCount;
        monstersCount =this.GetComponent<BattleSpawner>().monstersCount;
        herosList = GetComponent<BattleSpawner>().herosList;
        monstersList = GetComponent<BattleSpawner>().monstersList;
    }
    // Start is called before the first frame update
    void Start()
    {
        SpawnAllBar();
        SetUIPositionWithUnit();
    }

    private void LateUpdate()
    {
        SetSliderValueWithUnit();

    }
    // Update is called once per frame
    void Update()
    {
        //print(Bars.Count);
        
        
    }

    private void SpawnAllBar()
    {
        while (hasSpawnedBarCount <= GetComponent<BattleSpawner>().herosCount)//������������hero��Ѫ��
        {
            tempBar = Resources.Load<Slider>("Prefabs/UnitHP");
            tempBar = Instantiate(tempBar);
            tempBar.transform.SetParent(GameObject.Find("Canvas").transform, false);
            tempBar.name = "HeroHP" + hasSpawnedBarCount;//��˳���Ѫ������
            hasSpawnedBarCount++;
            heroBars.Add(tempBar);

        }
        hasSpawnedBarCount = 1;
        while (hasSpawnedBarCount <= GetComponent<BattleSpawner>().monstersCount)//������������monster��Ѫ��
        {
            tempBar = Resources.Load<Slider>("Prefabs/UnitHP");
            tempBar = Instantiate(tempBar);
            tempBar.transform.SetParent(GameObject.Find("Canvas").transform, false);
            tempBar.name = "MonsterHP" + hasSpawnedBarCount;//��˳���Ѫ������
            hasSpawnedBarCount++;
            monsterBars.Add(tempBar);

        }
        hasSpawnedBarCount = 1;
        while (hasSpawnedBarCount <= GetComponent<BattleSpawner>().herosCount)//������������
        {
            tempBar = Resources.Load<Slider>("Prefabs/UnitMP");
            tempBar = Instantiate(tempBar);
            tempBar.transform.SetParent(GameObject.Find("Canvas").transform, false);
            tempBar.name = "HeroMP" + hasSpawnedBarCount;//��˳�����������
            hasSpawnedBarCount++;
            heroBars.Add(tempBar);
        }
    }
}
