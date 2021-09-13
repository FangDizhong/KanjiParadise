using UnityEngine;
using System.Collections;
using System;
using System.Timers;

namespace Commons
{
    public class DelayToInvoke : MonoBehaviour
{
    public static IEnumerator DelayToInvokeDo(float delaySeconds, Action action)
    {
        yield return new WaitForSeconds(delaySeconds);
        action();
        }
    }
}