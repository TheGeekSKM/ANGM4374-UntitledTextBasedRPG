using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New IntSO", menuName = "Variables/IntSO")]
public class IntSO : ScriptableObject
{
    public int value;

    public UnityAction OnValueChanged;

    public void SetValue(int value)
    {
        this.value = value;
        OnValueChanged?.Invoke();
    }
    public void SetValue(IntSO value)
    {
        this.value = value.value;
        OnValueChanged?.Invoke();

    }

    public void Add(int value)
    {
        this.value += value;
        OnValueChanged?.Invoke();

    }
    public void Add(IntSO value)
    {
        this.value += value.value;
        OnValueChanged?.Invoke();

    }
}
