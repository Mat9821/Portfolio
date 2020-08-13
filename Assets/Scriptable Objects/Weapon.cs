using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ScriptableObject
{

    public string aWeaponName = "weapon name";

    public abstract void Initiliaze(GameObject obj);
    public abstract void Shoot();
    public abstract void Reload();
    public abstract void Equip();
}
