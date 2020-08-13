using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{

    public string abilityName = "ability name";

    public abstract void Initiliaze(GameObject obj);
    public abstract void ExecuteAbility();

}
