using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlerHomeScreen : MonoBehaviour
{
    [Header("Slider Background")]
    [SerializeField] private SliderBackground sliderBackground;

    void Start() {
        sliderBackground?.StartSlide();
    }
}
