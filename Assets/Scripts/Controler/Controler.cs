using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controler : MonoBehaviour {
    #region Day-Night System
    [Header("Day-Night System")]
    [SerializeField] private readonly byte numShifts = 3;
    private int contShifts;
    [SerializeField] private readonly bool initDay = true;
    [SerializeField] private GameObject Background;
    [SerializeField] private readonly float timeSecondTradeDayNight = 0.15f;
    private Coroutine coroutineTradeDayNight;
    private byte countCiclesForApply = 0;
    #endregion

    #region Stars System
    private byte countStars = 0;
    #endregion

    #region EVENTS
    public delegate void CompleatedStarInUI(byte index);
    public event CompleatedStarInUI compleatedStartInUI;
    public delegate void StartedCounterShifts(byte numShifts);
    public event StartedCounterShifts startedCounterShifts;
    public delegate void IncrementedOneShift();
    public event IncrementedOneShift incrementedOneShift;
    #endregion
    void Start() {
        startedCounterShifts?.Invoke(numShifts);
    }

    void Update() {

    }

    private IEnumerator tradeDayNight() {
        while (countCiclesForApply > 0){
            Quaternion startRotation = Background.transform.rotation;
            Quaternion endRotation = startRotation * Quaternion.Euler(0,0,-180);
            float countTime = 0;

            while (countTime <= timeSecondTradeDayNight) {
                countTime += Time.deltaTime;
                float rot = countTime / timeSecondTradeDayNight;

                Background.transform.rotation = Quaternion.Lerp(startRotation, endRotation, rot);
                yield return null;
            }
            Background.transform.rotation = endRotation;
            countCiclesForApply--;
        }
        coroutineTradeDayNight = null;
    }

    #region EVENTS
    public void OnCollectStar() {
        countStars++;
        compleatedStartInUI?.Invoke(countStars);
    }
    public void OnInteractAnything(IInteract interact) {

    }
    public void OnPlayerMove() {
        contShifts++;
        incrementedOneShift?.Invoke();
        if (contShifts >= numShifts) {
            countCiclesForApply++;
            contShifts = 0;
            if(coroutineTradeDayNight == null) coroutineTradeDayNight = StartCoroutine(tradeDayNight());
        }
    }
    #endregion
}
