using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHealth : MonoBehaviour
{
    UnitInteractions controller;
    Slider slider;
    
    private void Awake()
    {
        controller = FindFirstObjectByType<UnitInteractions>();
        slider = GetComponent<Slider>();
        slider.maxValue = controller.MaxSanity;
    }

    private void Update()
    {
        slider.value = controller.sanity;
    }

}
