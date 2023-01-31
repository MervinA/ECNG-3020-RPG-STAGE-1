using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public Item currentItem ; 
    public List<Item> items = new List<Item>();
    public int numberofkeys; 

    public void AddItem(Item itemToAdd)
    {
        // is the item a key? 
        if (itemToAdd.isKey)
        {
            numberofkeys++; 
        }else
        {
            if(!items.Contains(itemToAdd))
            {
                items.Add(itemToAdd);
            }
        }
    }


}