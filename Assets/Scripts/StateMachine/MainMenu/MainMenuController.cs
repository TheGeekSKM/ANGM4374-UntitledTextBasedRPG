using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] GameObject titlePanel;
    [SerializeField] GameObject buttonPanel;

    [SerializeField] GameObject creditsPanel;

    float titlePanelY;
    float buttonPanelY;
    float creditsPanelY;


    public void AnimateMenuIntro()
    {
        titlePanelY = titlePanel.GetComponent<RectTransform>().anchoredPosition.y;
        buttonPanelY = buttonPanel.GetComponent<RectTransform>().anchoredPosition.y;

        titlePanel.GetComponent<RectTransform>().DOAnchorPosY(0f, 1f).SetEase(Ease.OutCubic);
        buttonPanel.GetComponent<RectTransform>().DOAnchorPosY(476f, 1f).SetEase(Ease.OutCubic);
    }

    public void AnimateMenuOutro()
    {
        titlePanel.GetComponent<RectTransform>().DOAnchorPosY(titlePanelY, 1f).SetEase(Ease.OutCubic);
        buttonPanel.GetComponent<RectTransform>().DOAnchorPosY(buttonPanelY, 1f).SetEase(Ease.OutCubic);
    }

    public void AnimateCreditsIntro()
    {
        creditsPanel.SetActive(true);
        creditsPanelY = creditsPanel.GetComponent<RectTransform>().anchoredPosition.y;
        creditsPanel.GetComponent<RectTransform>().DOAnchorPosY(0f, 1f).SetEase(Ease.OutCubic);
    }

    public void AnimateCreditsOutro()
    {
        creditsPanel.GetComponent<RectTransform>().DOAnchorPosY(creditsPanelY, 1f).SetEase(Ease.OutCubic).OnComplete(() => creditsPanel.SetActive(false));
    }

    public void LoadPlayScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
