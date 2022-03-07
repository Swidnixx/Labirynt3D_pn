using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyType
{
    Red,
    Green,
    Gold
}
public class Key : Pickup
{
    public KeyType keyType;
    public override void Collect()
    {
        GameManager.instance.AddKey(keyType);
        Destroy(gameObject);
    }
}
