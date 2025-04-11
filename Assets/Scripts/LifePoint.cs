using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePoint : MonoBehaviour, Item
{
    public void Collect()
    {
        Destroy(gameObject);
    }
}
