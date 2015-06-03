using UnityEngine;
using System.Collections;
using UnityEditor;

public class CardItemUtility
{
    [MenuItem("Assets/Create/Card")]
    public static void CreateItem()
    {
        ScriptableObjectUtility.CreateAsset<AnimalCardData>();
    }
}
