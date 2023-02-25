using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PluginSettings : MonoBehaviour
{

    private const int maxTweens = 200;
    private const int maxSequences = 10;
    private const bool recycleAll = true;
    private const bool useSafeMode = true;

    void Awake()
    {
        DOTween.Init(recycleAll, useSafeMode, LogBehaviour.Default).SetCapacity(maxTweens, maxSequences);
    }
}
