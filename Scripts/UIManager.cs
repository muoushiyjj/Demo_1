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


    public enum UIStatus//����ö��
    {
        Attack,
        Defence,
        Medicine,
        Run,
        Attack_1,
        Attack_2,
        Back
    }
    private UIStatus uistatus;//����ö�ٱ���
    private UIStatus Status//�������Ը�ö�ٱ�����ֵ
    {
        get
        {
            return uistatus;
        }
        set
        {
            uistatus = value;
            UpdateUI();//�ڸ�ö�ٱ�����ֵ�����UI��ʾ����������UI����ʾ
        }
    }

    public void UpdateUI()//����UI��ʾ�ķ�����ͨ��ö�ٱ�����ֵ���ж�
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
        Status = UIStatus.Back;//��ʼ��һ��ui
        
    }


    private void Update()
    {
        UIMenu.SetActive(menuShow);
    }
}
