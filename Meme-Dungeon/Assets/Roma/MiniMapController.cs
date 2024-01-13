using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    public float moveSpeedX = 1f;  // Скорость движения элемента UI
    public float moveSpeedY = 1f;
    public float limitationTX = 300f;
    public float limitationTY = 300f;
    public Transform player;
    float koef = 1f;
    public float mapRazmer = 34f;

    private void Start()
    {
        koef = limitationTX / mapRazmer;
    }

    void Update()
    {
        // Получаем относительные координаты элемента UI в пространстве его родителя (или канваса)
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 relativeCoordinates = rectTransform.anchoredPosition;

        // Получаем координаты игрока в мировом пространстве
        Vector3 playerWorldPosition = player.position;

        // Выводим координаты игрока в консоль
        //Debug.Log("Player World Coordinates: " + playerWorldPosition);

        // Выводим относительные координаты в консоль
        Debug.Log("Relative Coordinates: " + relativeCoordinates + " Player World Coordinates: " + playerWorldPosition);


        // Вычисляем новую относительную координату X, с учетом скорости движения
        float newRelativeX = Mathf.Clamp(playerWorldPosition.x *koef, 0f, limitationTX);
        float newRelativeY = Mathf.Clamp(playerWorldPosition.z * koef, 0f, limitationTY);

        // Создаем новый вектор относительных координат с обновленной X-координатой
        Vector2 newRelativePosition = new Vector2(newRelativeX, newRelativeY);

        // Устанавливаем новые относительные координаты элемента UI
        rectTransform.anchoredPosition = newRelativePosition;
    }
}

/*

using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    public float moveSpeedX = 1f;  // Скорость движения элемента UI
    public float moveSpeedY = 1f;
    public float limitationTX = 300f;
    public float limitationTY = 300f;
    public Transform player;

    void Update()
    {
        // Получаем относительные координаты элемента UI в пространстве его родителя (или канваса)
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 relativeCoordinates = rectTransform.anchoredPosition;

        // Получаем координаты игрока в мировом пространстве
        Vector3 playerWorldPosition = player.position;

        // Выводим координаты игрока в консоль
        //Debug.Log("Player World Coordinates: " + playerWorldPosition);

        // Выводим относительные координаты в консоль
        Debug.Log("Relative Coordinates: " + relativeCoordinates + " Player World Coordinates: " + playerWorldPosition);


        // Вычисляем новую относительную координату X, с учетом скорости движения
        float newRelativeX = Mathf.Clamp(relativeCoordinates.x + moveSpeedX * Time.deltaTime, 0f, limitationTX);
        float newRelativeY = Mathf.Clamp(relativeCoordinates.y + moveSpeedY * Time.deltaTime, 0f, limitationTY);

        // Создаем новый вектор относительных координат с обновленной X-координатой
        Vector2 newRelativePosition = new Vector2(newRelativeX, newRelativeY);

        // Устанавливаем новые относительные координаты элемента UI
        rectTransform.anchoredPosition = newRelativePosition;
    }
}

*/


/*

using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    public float moveSpeed = 1f;  // Скорость движения элемента UI
    public float limitationTX = 300f;
    public float limitationTY = 300f;

    void Update()
    {
        // Получаем относительные координаты элемента UI в пространстве его родителя (или канваса)
        Vector2 relativeCoordinates = GetComponent<RectTransform>().anchoredPosition;

        // Выводим относительные координаты в консоль
        Debug.Log("Relative Coordinates: " + relativeCoordinates);
        // Получаем текущую позицию элемента UI
        Vector3 currentPosition = transform.position;

        // Вычисляем новую позицию, с учетом скорости движения
        float newX = currentPosition.x - moveSpeed * Time.deltaTime;
        Debug.Log(newX);
        
        if ( newX > limitationTX || newX < 0f)
        {
            newX = currentPosition.x;
        }
        
        Vector3 newPosition = new Vector3(newX, currentPosition.y, currentPosition.z);

        // Устанавливаем новую позицию элемента UI
        transform.position = newPosition;
    }
}

*/