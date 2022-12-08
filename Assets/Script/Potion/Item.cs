using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public enum ItemType  // J : ������ ����
    {
        Ingredient,
        RawMaterial,
        CraftedPotion,
    }

    public string itemName; // J : �̸�
    public ItemType itemType; // J : ����
    public Sprite itemImage; // J : �κ��丮�� ��Ÿ�� �̹���
}