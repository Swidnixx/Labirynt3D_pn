using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : Pickup
{
    public int freezeTime = 10;
    public override void Collect()
    {
        GameManager.instance.TimeFreeze(freezeTime);
        Destroy(gameObject);
    }
}
