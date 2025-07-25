using UnityEngine;
using System.Collections.Generic;
public class Weapon : MonoBehaviour
{
    public int weaponLevel;
    public List<WeaponStats> stats;


    [System.Serializable]
    public class WeaponStats
    {
        public float speed;
        public int damage;
        public float size;
        public float amount;
        public float range;
    }
}
