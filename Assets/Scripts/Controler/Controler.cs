using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controler : MonoBehaviour {
    [Header("Day-Night System")]
    [SerializeField] private readonly int numShifts = 3;
    private int contShifts;
    [SerializeField] private readonly bool initDay = true;
    [SerializeField] private GameObject Background;
    [SerializeField] private readonly float timeSecondTradeDayNight = 1;
    void Start() {

    }

    void Update() {

    }

    private IEnumerator tradeDayNight() {
        Quaternion startRotation = Background.transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0,0,180);
        float countTime = 0;

        while (countTime <= timeSecondTradeDayNight) {
            countTime += Time.deltaTime;
            float rot = countTime / timeSecondTradeDayNight;

            Background.transform.rotation = Quaternion.Lerp(startRotation, endRotation, rot);
            yield return null;
        }
        Background.transform.rotation = endRotation;
    }

    #region EVENTS
    public void OnCollectAnything(ICollect collect) {

    }
    public void OnInteractAnything(IInteract interact) {

    }
    public void OnPlayerMove() {
        contShifts++;
        Debug.Log(contShifts);
        if (contShifts >= numShifts) {
            StartCoroutine(tradeDayNight());
            contShifts = 0;
        }
    }
    #endregion
}
