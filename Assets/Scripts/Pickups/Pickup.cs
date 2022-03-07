using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    public virtual void Collect()
    {
        Debug.Log("Pickup collected");
        Destroy(gameObject);
    }
}
