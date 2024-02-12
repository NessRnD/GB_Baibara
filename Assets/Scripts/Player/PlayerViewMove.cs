using UnityEngine;

public class PlayerViewMove : MonoBehaviour
{
    private Controls controls; // подключаем систему ввода

    //переменные для камеры:
    [SerializeField] private float distance = 10f; // переменная для изменения отступа камеры от объекта
    [SerializeField] private float cameraSensivity = 5f; // чувствительность камеры
    [SerializeField] private float cameraYoffset = 5f;
    [SerializeField] private float cameraTopAngleBound = -80f;
    [SerializeField] private float cameraBotAngleBound = -15f;
    private Vector2 input_View; // переменная для хранения позиции ввод мыши
    private Vector3 cameraOffset; // положение камеры относительно объекта
    private float x; // переменная для хранения позиции x
    private float y; // переменная для хранения позиции y


    private void Awake() // выполняется до функции Start
    {
        controls = new Controls(); // поключаем управление
    
        controls.Player.Look.performed += context => input_View = context.ReadValue<Vector2>();
        controls.Player.Look.canceled += context => input_View = Vector2.zero;
    }

    private void Start() // выполняется перед прорисовкой первого кадра
    {
        cameraOffset = new Vector3(cameraOffset.x, cameraYoffset, -Mathf.Abs(distance));
        Camera.main.transform.position = transform.position + cameraOffset;
    }


    private void FixedUpdate() 
    {
        // обзор камерой:
        x = Camera.main.transform.localEulerAngles.y + input_View.x * cameraSensivity; // смещение камеры по X
        y += input_View.y * cameraSensivity; // смещение камеры по Y
        y = Mathf.Clamp(y, cameraTopAngleBound , cameraBotAngleBound); // ограничение вращения по Y
        Camera.main.transform.eulerAngles = new Vector3(-y, x, 0); // поворот камеры в углах Эйлера
        Camera.main.transform.position = Camera.main.transform.rotation * cameraOffset + transform.position; // непосредственно изменение положения
    }

    private void OnEnable() // активируем управление
    {
        controls.Player.Enable();
    }
    private void OnDisable() // дизактивируем управление
    {
        controls.Player.Disable();
    }
}
