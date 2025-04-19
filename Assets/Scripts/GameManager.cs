using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public Text scoreText;
    public Text winText;
    public int totalCollectibles = 5;
    
    private int collectedItems = 0;
    
    void Awake()
    {
        // Синглтон
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        // Создаем UI для счета
        CreateUI();
        
        // Скрываем текст победы
        winText.gameObject.SetActive(false);
        
        // Обновляем счет
        UpdateScore();
    }
    
    void CreateUI()
    {
        // Создаем Canvas
        GameObject canvas = new GameObject("GameCanvas");
        Canvas canvasComp = canvas.AddComponent<Canvas>();
        canvasComp.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.AddComponent<CanvasScaler>();
        canvas.AddComponent<GraphicRaycaster>();
        
        // Текст счета
        GameObject scoreObj = new GameObject("ScoreText");
        scoreObj.transform.SetParent(canvas.transform);
        scoreText = scoreObj.AddComponent<Text>();
        scoreText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        scoreText.fontSize = 24;
        scoreText.color = Color.white;
        scoreText.text = "Собрано: 0 / " + totalCollectibles;
        
        RectTransform scoreRect = scoreText.GetComponent<RectTransform>();
        scoreRect.anchoredPosition = new Vector2(100, -30);
        scoreRect.sizeDelta = new Vector2(200, 50);
        scoreRect.anchorMin = new Vector2(0, 1);
        scoreRect.anchorMax = new Vector2(0, 1);
        
        // Текст победы
        GameObject winObj = new GameObject("WinText");
        winObj.transform.SetParent(canvas.transform);
        winText = winObj.AddComponent<Text>();
        winText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        winText.fontSize = 48;
        winText.color = Color.green;
        winText.text = "Вы собрали все предметы!";
        winText.alignment = TextAnchor.MiddleCenter;
        
        RectTransform winRect = winText.GetComponent<RectTransform>();
        winRect.anchoredPosition = Vector2.zero;
        winRect.sizeDelta = new Vector2(600, 100);
        winRect.anchorMin = new Vector2(0.5f, 0.5f);
        winRect.anchorMax = new Vector2(0.5f, 0.5f);
    }
    
    public void CollectItem()
    {
        collectedItems++;
        UpdateScore();
        
        // Проверяем условие победы
        if (collectedItems >= totalCollectibles)
        {
            Win();
        }
    }
    
    void UpdateScore()
    {
        if (scoreText != null)
        {
            scoreText.text = "Собрано: " + collectedItems + " / " + totalCollectibles;
        }
    }
    
    void Win()
    {
        winText.gameObject.SetActive(true);
        
        // Через 3 секунды скрываем сообщение о победе
        Invoke("HideWinText", 3f);
    }
    
    void HideWinText()
    {
        winText.gameObject.SetActive(false);
    }
}