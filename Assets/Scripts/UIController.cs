using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject cardPanel;
    [SerializeField] GameController gameController;
    [SerializeField] Tower tower;

    [SerializeField] Text towerHpText;
    [SerializeField] Text currWaveText;
    [SerializeField] Text enemiesToNextWave;

    [SerializeField] Button firstCard;
    [SerializeField] Button secondCard;
    [SerializeField] Button thirdCard;
    [SerializeField] Button[] cardButtons;

    [SerializeField] Button gameOverButton;

    private Data data;
    private int[] cards = new int[] { 1, 1, 1, 1, 1 };

    void Start()
    {
        data = Resources.Load<Data>("Data");
        cardPanel.SetActive(false);
        gameController.endWave += ShowCardPanel;
        firstCard.onClick.AddListener(SelectedFirstCard);
        secondCard.onClick.AddListener(SelectedSecondCard);
        thirdCard.onClick.AddListener(SelectedThirdCard);
        gameOverButton.onClick.AddListener(RestartGame);
    }

    private void OnGUI()
    {
        towerHpText.text = "Tower HP: " + tower.GetTowerHp().ToString();
        currWaveText.text = "Wave: " + gameController.currWave.ToString();
        enemiesToNextWave.text = "Enemies until the next wave: " + gameController.enemyCounter.ToString();
    }

    void ShowCardPanel()
    {
        cardPanel.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            while (true)
            {
                var random = Random.Range(0, 5);
                if (cards[random] == 1)
                {
                    switch (random)
                    {
                        case 0:
                            cardButtons[i].transform.Find("Text").GetComponent<Text>().text = "Slowdown";
                            break;
                        case 1:
                            cardButtons[i].transform.Find("Text").GetComponent<Text>().text = "Fire Rate";
                            break;
                        case 2:
                            cardButtons[i].transform.Find("Text").GetComponent<Text>().text = "Plus damage";
                            break;
                        case 3:
                            cardButtons[i].transform.Find("Text").GetComponent<Text>().text = "Plus crit damage";
                            break;
                        case 4:
                            cardButtons[i].transform.Find("Text").GetComponent<Text>().text = "Plus tower HP";
                            break;
                    }
                    cards[random] = 0;
                    break;
                }
            }
        }
        cards = new int[] { 1, 1, 1, 1, 1 };
    }

    void HideCardPanel(string skill)
    {
        switch (skill)
        {
            case "Slowdown":
                data.SetSlowdown();
                break;
            case "Fire Rate":
                data.SetFireRate();
                break;
            case "Plus damage":
                data.SetDamage();
                break;
            case "Plus crit damage":
                data.SetCritDamage();
                break;
            case "Plus tower HP":
                tower.SetTowerHp(data.PlusTowerHealth());
                break;
        }
        gameController.nextWave.Invoke();
        cardPanel.SetActive(false);
    }

    void SelectedFirstCard()
    {
        HideCardPanel(firstCard.transform.Find("Text").GetComponent<Text>().text);
    }

    void SelectedSecondCard()
    {
        HideCardPanel(secondCard.transform.Find("Text").GetComponent<Text>().text);
    }

    void SelectedThirdCard()
    {
        HideCardPanel(thirdCard.transform.Find("Text").GetComponent<Text>().text);
    }

    void RestartGame()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
        data.SetDefaultParameters();
    }
}
