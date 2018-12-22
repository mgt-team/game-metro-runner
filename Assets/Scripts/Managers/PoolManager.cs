using UnityEngine;

public class PoolManager : Singleton<PoolManager> {
    public Pool CoinPool { get; private set; }
    public Pool NpcPool { get; private set; }
    public Pool ZonePool { get; private set; }

    private void Awake()
    {
        CoinPool = FindPoolByTag(TagEnum.CoinPool);
        CoinPool.Init();
        NpcPool = FindPoolByTag(TagEnum.NpcPool);
        NpcPool.Init();
        ZonePool = FindPoolByTag(TagEnum.ZonePool);
        ZonePool.Init();
    }

    private static Pool FindPoolByTag(TagEnum tagEnum)
    {
        return GameObject             
            .FindGameObjectWithTag(TagManager.GetTagNameByEnum(tagEnum))
            .GetComponent<Pool>();
    }
}
