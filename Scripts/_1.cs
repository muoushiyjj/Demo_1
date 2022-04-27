using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _1 : MonoBehaviour
{
    int i;
    void Print()
    {
        print("—”≥Ÿ¡À3√Î");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Main.DelayFuc(Print, 3f));
        print(i++);
    }
}
