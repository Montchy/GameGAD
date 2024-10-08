using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameManager")]
public class GameManager : ScriptableObject {

   public int _maxHealth;
   public int _currentHealth;
   public int _money;
   public int _xpToLvlUp;
   public int _food;
   public int _level;
   public int _armorMP;
   public int _damageMP;


   public bool _canMove;
   public bool _gameFreeze;
   public GameObject _getHolded;



   public Vector3 _playerPos;


}

