using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsManagerCount : MonoBehaviour {
    void OnValidate() {
        ICollect[] listStars = gameObject.GetComponentsInChildren<ICollect>();
        if(listStars.Length != 3) Debug.LogError($"The level needs to have 3 stars, but this level has {listStars.Length} stars.");
    }
}
