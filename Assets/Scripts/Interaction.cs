using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public float interactionDistance = 2f;
    public GameObject interactionPrompt;
    
    private InteractiveObject currentObject;
    private Camera playerCamera;
    
    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
        
        // Создаем UI для подсказки
        if (interactionPrompt == null)
        {
            CreateInteractionPrompt();
        }
    }
    
    void CreateInteractionPrompt()
    {
        // Создаем канвас для UI
        GameObject canvas = new GameObject("InteractionCanvas");
        Canvas canvasComp = canvas.AddComponent<Canvas>();
        canvasComp.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.AddComponent<CanvasScaler>();
        canvas.AddComponent<GraphicRaycaster>();
        
        // Создаем текстовый объект
        GameObject textObj = new GameObject("InteractionText");
        textObj.transform.SetParent(canvas.transform);
        Text text = textObj.AddComponent<Text>();
        text.text = "Нажмите E для взаимодействия";
        text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf"); // Исправленный шрифт
        text.fontSize = 24;
        text.color = Color.white;
        text.alignment = TextAnchor.MiddleCenter;
        
        // Настраиваем позицию текста
        RectTransform rectTransform = text.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, -50);
        rectTransform.sizeDelta = new Vector2(400, 50);
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        
        // Создаем фон для текста
        GameObject background = new GameObject("Background");
        background.transform.SetParent(textObj.transform);
        Image image = background.AddComponent<Image>();
        image.color = new Color(0, 0, 0, 0.5f);
        
        RectTransform bgRectTransform = image.GetComponent<RectTransform>();
        bgRectTransform.anchorMin = new Vector2(0, 0);
        bgRectTransform.anchorMax = new Vector2(1, 1);
        bgRectTransform.offsetMin = new Vector2(-10, -10);
        bgRectTransform.offsetMax = new Vector2(10, 10);
        
        // Важно: сначала сохраняем ссылку
        interactionPrompt = textObj;
        
        // А теперь скрываем подсказку
        interactionPrompt.SetActive(false);
    }
    
    void Update()
    {
        // Проверяем наличие объекта для взаимодействия
        CheckForInteractiveObject();
        
        // Если нажата E и есть объект для взаимодействия
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Нажата клавиша E");
            if (currentObject != null)
            {
                Debug.Log("Взаимодействие с объектом: " + currentObject.name);
                currentObject.Interact();
            }
            else
            {
                Debug.Log("Объект для взаимодействия не найден");
            }
        }
    }
    
    void CheckForInteractiveObject()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            InteractiveObject interactiveObject = hit.collider.GetComponent<InteractiveObject>();
            
            if (interactiveObject != null)
            {
                // Показываем подсказку, если нашли объект
                interactionPrompt.SetActive(true);
                currentObject = interactiveObject;
                return;
            }
        }
        
        // Скрываем подсказку, если объекта нет
        interactionPrompt.SetActive(false);
        currentObject = null;
    }
}