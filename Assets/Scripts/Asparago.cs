using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asparago : MonoBehaviour, Item
{
    public void Collect()
    {
        Destroy(gameObject);
    }
}
