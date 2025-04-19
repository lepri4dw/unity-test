using UnityEngine;

public class Collectible : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float hoverHeight = 0.2f;
    public GameObject collectEffect;
    
    private Vector3 startPosition;
    
    void Start()
    {
        startPosition = transform.position;
    }
    
    void Update()
    {
        // Вращаем предмет
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        
        // Плавно перемещаем вверх-вниз
        float newY = startPosition.y + Mathf.Sin(Time.time * 2f) * hoverHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Увеличиваем счетчик в GameManager
            GameManager.instance.CollectItem();
            
            // Эффект сбора
            if (collectEffect != null)
            {
                Instantiate(collectEffect, transform.position, Quaternion.identity);
            }
            
            // Уничтожаем предмет
            Destroy(gameObject);
        }
    }
}