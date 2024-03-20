using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public int Value { get; set; }
    public int DropChance { get; set; }
    public Sprite LootSprite { get; set; }
    public string LootName { get; set; }

    public Loot(string lootName, int dropChance, int value)
    {
        this.LootName = lootName;
        this.DropChance = dropChance;
        this.Value = value;
    }
}
