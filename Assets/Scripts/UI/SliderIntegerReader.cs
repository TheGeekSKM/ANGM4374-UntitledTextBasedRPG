using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderIntegerReader : MonoBehaviour
{
    [SerializeField] IntSO intSO;
    [SerializeField] Slider healthSlider;

    private void OnValidate()
    {
        if (healthSlider == null) healthSlider = GetComponent<Slider>();
    }

    private void OnEnable() {
        if (intSO) intSO.OnValueChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        if (intSO) intSO.OnValueChanged -= OnValueChanged;
    }

    private void Start()
    {
        healthSlider.maxValue = intSO.value;
    }

    void OnValueChanged()
    {
        // Debug.Log("Health Bar Changed");
        healthSlider.value = intSO.value;
    }
}
