using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour, ICollect
{
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void collected(Player player) {
        Destroy(this.gameObject);
    }

}
