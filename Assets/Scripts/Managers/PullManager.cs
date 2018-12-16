using UnityEngine;

public class PullManager : Singleton<PullManager> {

    [SerializeField] private Pull _coinPull;
    [SerializeField] private Pull _npcPull;

    public Pull CoinPull
    {
        get
        {
            return _coinPull;
        }
    }

    public Pull NpcPull
    {
        get
        {
            return _npcPull;
        }
    }
}
