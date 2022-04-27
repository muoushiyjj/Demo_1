using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompareUnitsSpeed : IComparer<GameObject>//һ��ʵ���˱Ƚϵĳ�����
{
    public int Compare(GameObject x, GameObject y)
    {
        if (x.GetComponent<UnitStats>().speed> y.GetComponent<UnitStats>().speed) return -1;
        if (x.GetComponent<UnitStats>().speed < y.GetComponent<UnitStats>().speed) return 1;
        return 0;
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
