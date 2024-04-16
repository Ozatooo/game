using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public int Value;
    public int DropChance;
    public Sprite LootSprite;
    public string LootName;

    public Loot(string lootName, int dropChance, int value)
    {
        this.LootName = lootName;
        this.DropChance = dropChance;
        this.Value = value;
    }
}
