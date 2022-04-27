using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject UIMenu;
    public GameObject UIBottomAttack;
    public GameObject UIBottomDefence;
    public GameObject UIBottomMedicine;
    public GameObject UIBottomRun;
    public GameObject UIBottomAttack_1;
    public GameObject UIBottomAttack_2;
    public GameObject UIBottomBack;
    public bool menuShow=true;


    public enum UIStatus//定义枚举
    {
        Attack,
        Defence,
        Medicine,
        Run,
        Attack_1,
        Attack_2,
        Back
    }
    private UIStatus uistatus;//创建枚举变量
    private UIStatus Status//定义属性给枚举变量赋值
    {
        get
        {
            return uistatus;
        }
        set
        {
            uistatus = value;
            UpdateUI();//在给枚举变量赋值后调用UI显示方法，控制UI的显示
        }
    }

    public void UpdateUI()//定义UI显示的方法，通过枚举变量的值来判断
    {
        
        UIBottomAttack.SetActive(uistatus == UIStatus.Back);
        UIBottomDefence.SetActive(uistatus == UIStatus.Back);
        UIBottomMedicine.SetActive(uistatus == UIStatus.Back);
        UIBottomRun.SetActive(uistatus == UIStatus.Back);
        UIBottomAttack_1.SetActive(uistatus == UIStatus.Attack);
        UIBottomAttack_2.SetActive(uistatus == UIStatus.Attack);
        UIBottomBack.SetActive(uistatus == UIStatus.Attack);
    }

    public void Attack1()
    {
        Status = UIStatus.Back;
    }
    public void Attack()
    {
        Status = UIStatus.Attack;
    }
    public void Back()
    {
        Status = UIStatus.Back;
    }
    private void Start()
    {
        Status = UIStatus.Back;//初始化一次ui
        
    }


    private void Update()
    {
        UIMenu.SetActive(menuShow);
    }
}
