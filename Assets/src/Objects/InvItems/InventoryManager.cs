using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager Instance;
    public List<InventoryItem> Items = new List<InventoryItem>();
    public Dictionary<string, int> ItemCountDictionary = new Dictionary<string, int>();


    public Transform ItemContent;
    public GameObject InventoryItem;

    void Awake()
    {
        Instance = this;
        ListItems();
    }

   public void Add(InventoryItem item){

    if (Items.Exists(existingItem => existingItem._Objectname == item._Objectname))
    {

        ItemCountDictionary[item._Objectname] = ItemCountDictionary[item._Objectname]+1;
        int c = ItemCountDictionary[item._Objectname];

        Debug.Log("Raised "+item._Objectname+" / "+c);
    


       
    }else{
        Debug.Log(item._Objectname);
        Items.Add(item);
        ItemCountDictionary.Add(item._Objectname,1);
        Debug.Log("Added "+item._Objectname);
       }

   }

   public void Remove(InventoryItem item){
    Items.Remove(item);
   }

   public void ListItems(){

    foreach(Transform item in ItemContent){
        Destroy(item.gameObject);
    }

    foreach(var item in Items){

        GameObject obj = Instantiate(InventoryItem,ItemContent);
        var itemName = obj.transform.Find("Objectname").GetComponent<TextMeshProUGUI>();
        var itemIcon = obj.transform.Find("Icon").GetComponent<Image>();
        var itemCount = obj.transform.Find("Count").GetComponent<TextMeshProUGUI>();

        itemCount.text = ""+ItemCountDictionary[item._Objectname];
        itemName.text=item._Objectname;
        itemIcon.sprite=item._Icon;

    }
   }
}