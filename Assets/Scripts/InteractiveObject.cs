using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public Color originalColor = Color.white;
    public Color interactedColor = Color.green;
    public float hoverHeight = 0.5f;
    public float rotationSpeed = 30f;
    
    private Renderer objectRenderer;
    private bool hasInteracted = false;
    private Vector3 startPosition;
    private Light objectLight;
    
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.material.color = originalColor;
        startPosition = transform.position;
        
        // Добавляем свет к объекту
        GameObject lightObj = new GameObject("ObjectLight");
        lightObj.transform.SetParent(transform);
        lightObj.transform.localPosition = Vector3.zero;
        
        objectLight = lightObj.AddComponent<Light>();
        objectLight.type = LightType.Point;
        objectLight.range = 2f;
        objectLight.intensity = 0f; // Начинаем с выключенного света
        objectLight.color = new Color(1f, 0.8f, 0.4f); // Теплый желтый свет
    }
    
    void Update()
    {
        if (hasInteracted)
        {
            // Вращаем объект
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            
            // Перемещаем объект вверх и вниз
            float newY = startPosition.y + Mathf.Sin(Time.time) * hoverHeight;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
    
    public void Interact()
    {
        hasInteracted = !hasInteracted;
        
        if (hasInteracted)
        {
            objectRenderer.material.color = interactedColor;
            objectLight.intensity = 1.5f; // Включаем свет
            Debug.Log("Вы активировали объект: " + gameObject.name);
        }
        else
        {
            objectRenderer.material.color = originalColor;
            objectLight.intensity = 0f; // Выключаем свет
            transform.position = startPosition; // Возвращаем исходную позицию
            Debug.Log("Вы деактивировали объект: " + gameObject.name);
        }
    }
}