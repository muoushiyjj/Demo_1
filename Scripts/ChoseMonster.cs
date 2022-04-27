using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoseMonster : MonoBehaviour
{
    private GameObject ChoseCanvas;


    public float xoffset;
    public float yoffset;

    private Button tempButton;
    private List<GameObject> monstersList = new List<GameObject>();
    private List<Button> choseMonsterButtonsList = new List<Button>();
    // Start is called before the first frame update

    private int hasSpawnedButtonsCount=1;
    
    private int index;

    private void SpawnAllButtom()
    {
        while (hasSpawnedButtonsCount <= GetComponent<BattleSpawner>().monstersCount)
        {
            tempButton = Resources.Load<Button>("Prefabs/ChoseMonsterButton");
            tempButton = Instantiate(tempButton);
            tempButton.transform.SetParent(GameObject.Find("ChoseMonsterCanvas").transform, false);
            tempButton.name = "ChoseMonster" + hasSpawnedButtonsCount;
            hasSpawnedButtonsCount++;
            choseMonsterButtonsList.Add(tempButton);
        }
        
    }

    private void SetChoseButtonOnClick()
    {
        while(index <= choseMonsterButtonsList.Count-1)
        {
            int tempindex=index;
            choseMonsterButtonsList[index].onClick.AddListener(() => { GetComponent<BattleManage>().ChoseMonster(tempindex); });//把index通过点击事件传给battlemanage
            index++;
        }
        index = 0;
    }
    private void Awake()
    {
        index = 0;
        monstersList = GetComponent<BattleSpawner>().monstersList;
        ChoseCanvas = GameObject.Find("ChoseMonsterCanvas");
        


    }

    private void SetButtonPositionWithMonster()
    {
        while (index <= monstersList.Count - 1)
        {
            choseMonsterButtonsList[index].transform.position = monstersList[index].transform.position + new Vector3(xoffset, yoffset, 0);
            index++;
            print("位置设置成功");
        }
        index = 0;
    }
    void Start()
    {
        SpawnAllButtom();
        SetButtonPositionWithMonster();
        SetChoseButtonOnClick();
        ChoseCanvas.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
