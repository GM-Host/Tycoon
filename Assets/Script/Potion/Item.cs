using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public abstract class Item : ScriptableObject
{
    public enum ItemType  // J : 아이템 유형
    {
        Potion,
        Cook,
    }

    public string itemName; // J : 이름
    public ItemType itemType; // J : 유형
    public Sprite itemImage; // J : 인벤토리에 나타날 이미지
}

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/Potion item")]
public class PotionItem : Item
{
    public enum StateType
    {
        Ingredient,     // J : 가공 전 재료
        RawMaterial,    // J : 가공 후 재료
        CraftedPotion,  // J : 포션
    }

    public enum ProcessType // J : 가공 재료의 타입
    {
        None,   // J : Ingredient, CraftdPotion
        Mill,   // J : 갈기
        Boil,   // J : 끓기
        Grind,  // J : 빻기
    }

    public StateType state;
    public ProcessType process;
}

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/Cook item")]
public class CookItem : Item
{
    
}