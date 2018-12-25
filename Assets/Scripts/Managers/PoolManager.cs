using UnityEngine;

public class PoolManager : Singleton<PoolManager> {
    
	public Pool CoinPool { get; private set; }
    public Pool NpcPool { get; private set; }
    public Pool ZonePool { get; private set; }

    private void Awake()
    {
        // Initialize all your pools
        CoinPool = InitPool(TagEnum.CoinPool);
        NpcPool = InitPool(TagEnum.NpcPool);
        ZonePool = InitPool(TagEnum.ZonePool);
    }

    private static Pool InitPool(TagEnum tagEnum)
    {
        var pool = GameObject             
            .FindGameObjectWithTag(TagManager.GetTagNameByEnum(tagEnum))
            .GetComponent<Pool>();
        pool.Init();
        return pool;
    }
}
