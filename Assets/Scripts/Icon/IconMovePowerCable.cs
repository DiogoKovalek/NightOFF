using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconMovePowerCable : MonoBehaviour
{
    [SerializeField] private float speedIcons = 3;
    private Vector3 startP;
    private Vector3 endP;
    private Vector3 curveP;
    private bool isActive = true;

    private float t = 0;
    void Update()
    {
        MoveIcon();
    }
    private void MoveIcon() {
        t += Time.deltaTime / speedIcons;
        transform.localPosition = bezier(t, startP, curveP, endP);
        if(t >= 1) StopMovement();
    }
    private Vector3 bezier(float t, Vector3 pStart, Vector3 pMiddle, Vector3 pEnd) {
        if(t > 1) t = 1;
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        return (uu * pStart) + (2 * u * t * pMiddle) + (tt * pEnd);
    }
    public void StartMovement(Vector3 startP, Vector3 endP, Vector3 curveP) {
        isActive = true;
        gameObject.SetActive(true);
        t = 0;
        this.startP = startP;
        this.endP = endP;
        this.curveP = curveP;
        transform.position = startP;
    }
    public void StopMovement() {
        isActive = false;
        gameObject.SetActive(false);
    }
    public bool IsActive() {
        return isActive;
    }
}

