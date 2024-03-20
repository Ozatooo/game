using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public GameObject DroppedItemPrefab;
    public List<Loot> LootPool = new List<Loot>();

    private List<Loot> GetDroppedItems()
    {
        if (LootPool?.Any() != true)
        {
            Debug.Log("LootBag is either empty or null");
            return null;
        }

        GetPossibleNumberOfDrops(LootPool, out int totalItemDropped);

        if (totalItemDropped == 0)
        {
            Debug.Log("No loot dropped - not lucky, huh?");
            return null;
        }

        LootPool.Sort((a, b) => b.DropChance.CompareTo(a.DropChance));
        GetTotalWeight(LootPool, out int totalWeight);
        List<Loot> LootList = new List<Loot>();

        for (int i = 0; i < totalItemDropped; i++)
        {
            int randomNumber = Random.Range(0, totalWeight);
            foreach (var item in LootPool)
            {
                if (randomNumber <= item.DropChance)
                {
                    LootList.Add(item);
                    break;
                }
                else
                {
                    randomNumber -= item.DropChance;
                }
            }
        }

        return LootList;
    }

    private void GetPossibleNumberOfDrops(List<Loot> lootPool, out int result)
    {
        result = Random.Range(0, lootPool.Count + 1);
    }

    private void GetTotalWeight(List<Loot> lootPool, out int result)
    {
        result = 0;
        foreach (Loot loot in lootPool)
        {
            result += loot.DropChance;
        }
    }

    private GameObject CreateLootGameObejct(Loot loot, Vector3 spawnPosition)
    {
        GameObject lootGameObject = Instantiate(DroppedItemPrefab, spawnPosition, Quaternion.identity);
        Loot lootComp = lootGameObject.GetComponent<Loot>();

        lootComp.GetComponent<SpriteRenderer>().sprite = loot.LootSprite;
        lootComp.LootName = loot.LootName;
        lootComp.Value = loot.Value;
        lootComp.DropChance = loot.DropChance;

        return lootGameObject;
    }
    public void InstantiateLoot(Vector3 spawnPosition)
    {
        List<Loot> droppedItems = GetDroppedItems();
        if (droppedItems != null)
        {
            foreach (Loot loot in droppedItems)
            {
                GameObject lootGameObject = CreateLootGameObejct(loot, spawnPosition);

                float dropForce = (float)Random.Range(100, 201);
                Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                lootGameObject.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce, ForceMode2D.Impulse);
            }
        }
    }
}
