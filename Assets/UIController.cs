using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject cardPanel;
    [SerializeField] GameController gameController;

    [SerializeField] Button firstCard;
    [SerializeField] Button secondCard;
    [SerializeField] Button thirdCard;
    private Data data;

    void Start()
    {
        data = Resources.Load<Data>("Data");
        cardPanel.SetActive(false);
        gameController.endWave += ShowCardPanel;
        firstCard.onClick.AddListener(SelectedFirstCard);
        secondCard.onClick.AddListener(SelectedSecondCard);
        thirdCard.onClick.AddListener(SelectedThirdCard);
    }

    void Update()
    {

    }

    void ShowCardPanel()
    {
        cardPanel.SetActive(true);
    }

    void HideCardPanel()
    {
        gameController.nextWave.Invoke();
        cardPanel.SetActive(false);
    }

    void SelectedFirstCard()
    {
        Debug.Log("first");
        data.slow = true;
        HideCardPanel();
    }

    void SelectedSecondCard()
    {
        Debug.Log("second");
        HideCardPanel();
    }

    void SelectedThirdCard()
    {
        Debug.Log("third");
        HideCardPanel();
    }
}
