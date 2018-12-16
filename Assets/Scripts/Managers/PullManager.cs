using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullManager : MonoBehaviour {

    [SerializeField] private Pull _coinPull;
    [SerializeField] private Pull _NPCPull;

    public Pull CoinPull
    {
        get
        {
            return _coinPull;
        }
    }

    public Pull NPCPull
    {
        get
        {
            return _NPCPull;
        }
    }
}
