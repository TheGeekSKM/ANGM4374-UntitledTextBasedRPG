using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering;

public class AttributeController : MonoBehaviour
{
    private AttributeFSM attributeFSM;

    [SerializeField] GameObject attributeMenuPanel;
    [SerializeField] AttributeData attributeData;
    [SerializeField] int possibleAttributePoints;
    [SerializeField] TextMeshProUGUI possibleAttributePointsText;
    [SerializeField] TextMeshProUGUI vitalityText;
    [SerializeField] TextMeshProUGUI agilityText;
    [SerializeField] TextMeshProUGUI intelligenceText;
    float attributeMenuPanelXPos;

    public void OnValidate()
    {
        if (attributeFSM == null)
        {
            attributeFSM = GetComponent<AttributeFSM>();
        }
    }

    void Start()
    {
        possibleAttributePointsText.text = $"You have {possibleAttributePoints} more points to assign.";
        vitalityText.text = attributeData.Vitality.ToString();
        agilityText.text = attributeData.Agility.ToString();
        intelligenceText.text = attributeData.Intelligence.ToString();
    }

    public void AnimateAttributeMenuIntro()
    {
        attributeMenuPanelXPos = attributeMenuPanel.GetComponent<RectTransform>().anchoredPosition.x;
        // attributeMenuPanel.transform.DOMoveX(0f, 0.5f).SetEase(Ease.OutCubic);
        attributeMenuPanel.GetComponent<RectTransform>().DOAnchorPosX(0f, 0.5f).SetEase(Ease.OutCubic);
        Debug.Log("AnimateAttributeMenuIntro");
    }

    public void AnimateAttributeMenuOutro()
    {
        attributeMenuPanel.GetComponent<RectTransform>().DOAnchorPosX(attributeMenuPanelXPos, 0.5f).SetEase(Ease.OutCubic).OnComplete(LoadGameScene);
        Debug.Log("AnimateAttributeMenuOutro");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void AddVitality(int amount)
    {
        if (amount > 0 && possibleAttributePoints > 0 && attributeData.Vitality < attributeData.VitalityRange.y)
        {
            possibleAttributePoints--;
            possibleAttributePointsText.text = $"You have {possibleAttributePoints} more points to assign.";

            attributeData.AddAttribute(Attributes.Vitality, amount);
            vitalityText.text = attributeData.Vitality.ToString();
        }
        else if (amount < 0 && attributeData.Vitality > attributeData.VitalityRange.x)
        {
            possibleAttributePoints++;
            possibleAttributePointsText.text = $"You have {possibleAttributePoints} more points to assign.";

            attributeData.AddAttribute(Attributes.Vitality, amount);
            vitalityText.text = attributeData.Vitality.ToString();
        }
        
        if (possibleAttributePoints <= 0)
        {
            AnimateAttributeMenuOutro();
        }
    }

    public void AddAgility(int amount)
    {
        if (amount > 0 && possibleAttributePoints > 0 && attributeData.Agility < attributeData.AgilityRange.y)
        {
            possibleAttributePoints--;
            possibleAttributePointsText.text = $"You have {possibleAttributePoints} more points to assign.";

            attributeData.AddAttribute(Attributes.Agility, amount);
            agilityText.text = attributeData.Agility.ToString();
        }
        else if (amount < 0 && attributeData.Agility > attributeData.AgilityRange.x)
        {
            possibleAttributePoints++;
            possibleAttributePointsText.text = $"You have {possibleAttributePoints} more points to assign.";

            attributeData.AddAttribute(Attributes.Agility, amount);
            agilityText.text = attributeData.Agility.ToString();
        }
        
        if (possibleAttributePoints <= 0)
        {
            AnimateAttributeMenuOutro();
        }
    }

    public void AddIntelligence(int amount)
    {
        if (amount > 0 && possibleAttributePoints > 0 && attributeData.Intelligence < attributeData.IntelligenceRange.y)
        {
            possibleAttributePoints--;
            possibleAttributePointsText.text = $"You have {possibleAttributePoints} more points to assign.";

            attributeData.AddAttribute(Attributes.Intelligence, amount);
            intelligenceText.text = attributeData.Intelligence.ToString();
        }
        else if (amount < 0 && attributeData.Intelligence > attributeData.IntelligenceRange.x)
        {
            possibleAttributePoints++;
            possibleAttributePointsText.text = $"You have {possibleAttributePoints} more points to assign.";

            attributeData.AddAttribute(Attributes.Intelligence, amount);
            intelligenceText.text = attributeData.Intelligence.ToString();
        }

        if (possibleAttributePoints <= 0)
        {
            AnimateAttributeMenuOutro();
        }
    }
}
