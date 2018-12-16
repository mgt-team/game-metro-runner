using UnityEngine;

public class PoolManager : Singleton<PoolManager> {
    public Pool CoinPool { get; private set; }

    public Pool NpcPool { get; private set; }

    private void Awake()
    {
        CoinPool = GameObject             
            .FindGameObjectWithTag(TagManager.GetTagNameByEnum(TagEnum.CoinPool))
            .GetComponent<Pool>();    
        
        NpcPool = GameObject
            .FindGameObjectWithTag(TagManager.GetTagNameByEnum(TagEnum.NpcPool))
            .GetComponent<Pool>();
    }
}
