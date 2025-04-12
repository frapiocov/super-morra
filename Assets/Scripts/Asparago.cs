using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Asparago : MonoBehaviour, Item
{
    public GameManager GM;

    public void Collect()
    {
        GM.IncreaseCollection();
        Destroy(gameObject);
    }
}
