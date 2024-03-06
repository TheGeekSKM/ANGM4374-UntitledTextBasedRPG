using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Attributes
{
    Vitality,
    Agility,
    Intelligence
}

[CreateAssetMenu(fileName = "AttributeData", menuName = "AttributeData")]
public class AttributeData : ScriptableObject
{
    [Header("Strength")]
    public int Vitality = 5;
    public Vector2Int VitalityRange;

    [Header("Agility")]
    public int Agility = 5;
    public Vector2Int AgilityRange;

    [Header("Intelligence")]
    public int Intelligence = 5;
    public Vector2Int IntelligenceRange;

    public UnityAction<Attributes> OnAttributeChange;

    public void OnValidate()
    {
        Vitality = Mathf.Clamp(Vitality, VitalityRange.x, VitalityRange.y);
        Agility = Mathf.Clamp(Agility, AgilityRange.x, AgilityRange.y);
        Intelligence = Mathf.Clamp(Intelligence, IntelligenceRange.x, IntelligenceRange.y);
    }

    public void AddAttribute(Attributes attribute, int value)
    {
        switch (attribute)
        {
            case Attributes.Vitality:
                Vitality = Mathf.Clamp(Vitality + value, VitalityRange.x, VitalityRange.y);
                OnAttributeChange?.Invoke(Attributes.Vitality);
                break;
            case Attributes.Agility:
                Agility = Mathf.Clamp(Agility + value, AgilityRange.x, AgilityRange.y);
                OnAttributeChange?.Invoke(Attributes.Agility);
                break;
            case Attributes.Intelligence:
                Intelligence = Mathf.Clamp(Intelligence + value, IntelligenceRange.x, IntelligenceRange.y);
                OnAttributeChange?.Invoke(Attributes.Intelligence);
                break;
        }
    }
}
