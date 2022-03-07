using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : Pickup
{
    public int points = 5;

    public override void Collect()
    {
        Debug.Log("Diamond collected");
        GameManager.instance.AddPoints(points);
        Destroy(gameObject);
    }
}
