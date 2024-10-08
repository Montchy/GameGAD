using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


[CreateAssetMenu(menuName = "InventoryItem")]
public class InventoryItem : ScriptableObject
{

    public string _Objectname;
    public Sprite _Icon;
    public InvItemType _type;

    public int _typescore;
}
