using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 100f;
    
    private CharacterController controller;
    private float xRotation = 0f;
    private Camera playerCamera;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        
        // Если компонент CharacterController отсутствует, добавляем его
        if (controller == null)
        {
            controller = gameObject.AddComponent<CharacterController>();
        }
        
        // Получаем камеру
        playerCamera = GetComponentInChildren<Camera>();
        
        // Блокируем и скрываем курсор мыши
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        // Управление мышью для камеры
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        // Поворот камеры вверх/вниз (с ограничением)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        // Применяем поворот к камере
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        // Поворот игрока влево/вправо
        transform.Rotate(Vector3.up * mouseX);
        
        // Движение с помощью WASD
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        // Направление движения относительно ориентации игрока
        Vector3 move = transform.right * x + transform.forward * z;
        
        // Перемещаем персонажа
        controller.Move(move * moveSpeed * Time.deltaTime);
    }
}