using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToBattle : MonoBehaviour
{
    public string monsterName1;
    public string monsterName2;
    public string monsterName3;

    public int count;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            SceneManager.LoadScene(1);
            PlayerPrefs.SetInt("MonstersCount",count);
            PlayerPrefs.SetString("MonsterName1", monsterName1);
            PlayerPrefs.SetString("MonsterName2", monsterName2);
            PlayerPrefs.SetString("MonsterName3", monsterName3);
            PlayerPrefs.SetInt("HerosCount", collision.gameObject.GetComponent<PlayerInformation>().heroCount);
            PlayerPrefs.SetString("HeroName1", collision.gameObject.GetComponent<PlayerInformation>().heroName1);
            PlayerPrefs.SetString("HeroName2", collision.gameObject.GetComponent<PlayerInformation>().heroName2);
            PlayerPrefs.SetString("HeroName3", collision.gameObject.GetComponent<PlayerInformation>().heroName3);


        }
    }
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
