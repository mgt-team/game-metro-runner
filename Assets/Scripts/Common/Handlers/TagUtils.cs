using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class TagUtils
{

    public static string GetTagNameByEnum<T>(T value)
    {
        return Enum.GetName(typeof(T), value);
    }

    public static bool CompareTagWithTagsList<T>(string tagToCompare, IEnumerable<T> tagEnumList)
    {
        return tagEnumList.Select(GetTagNameByEnum)
            .Any(tag => tag.Equals(tagToCompare));
    }

    public static bool CompareGameObjectTag(GameObject gameObject, TagEnum tagToCheck)
    {
        return gameObject.CompareTag(GetTagNameByEnum(tagToCheck));
    }
}


