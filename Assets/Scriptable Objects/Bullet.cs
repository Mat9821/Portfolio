using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : ScriptableObject
{
    public string aBulletName = "weapon name";
    // Start is called before the first frame update
    public abstract void Initiliaze(GameObject obj);
    public abstract void Instantiate();


}
