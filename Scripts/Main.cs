using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Main
{
    public static IEnumerator DelayFuc(Action action, float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        action();
    }
    public static IEnumerator Delay(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
    }
}
