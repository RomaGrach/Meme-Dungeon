using UnityEngine;
using UnityEngine.UI;

public class MiniMapControllerPRO : MonoBehaviour
{
    public float limitationTX = 300f;
    public float limitationTY = 300f;
    public Transform player;
    float koef = 1f;
    public float mapRazmer = 34f;

    public RawImage imageToDrawOn;

    private RectTransform canvasRect;
    private Texture2D canvasTexture;
    private Vector2 lastPosition;
    public float brushRadius = 5f; // Размер кисти

    void Start()
    {
        koef = limitationTX / mapRazmer;
        canvasRect = imageToDrawOn.GetComponent<RectTransform>();
        InitializeCanvasTexture();
    }

    void Update()
    {
        UpdateMiniMapPosition();
        EraseUnderPlayer();
        canvasTexture.Apply();
    }

    void UpdateMiniMapPosition()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 relativeCoordinates = rectTransform.anchoredPosition;

        Vector3 playerWorldPosition = player.position;

        float newRelativeX = Mathf.Clamp(playerWorldPosition.x * koef, 0f, limitationTX);
        float newRelativeY = Mathf.Clamp(playerWorldPosition.z * koef, 0f, limitationTY);

        Vector2 newRelativePosition = new Vector2(newRelativeX, newRelativeY);

        rectTransform.anchoredPosition = newRelativePosition;
    }

    void InitializeCanvasTexture()
    {
        canvasTexture = new Texture2D((int)canvasRect.rect.width, (int)canvasRect.rect.height);

        for (int x = 0; x < canvasTexture.width; x++)
        {
            for (int y = 0; y < canvasTexture.height; y++)
            {
                canvasTexture.SetPixel(x, y, Color.gray);
            }
        }

        imageToDrawOn.texture = canvasTexture;
        canvasTexture.Apply();
    }

    void EraseUnderPlayer()
    {
        Vector2 playerPosition = new Vector2(
            player.position.x * koef,
            player.position.z * koef
        );

        int x = Mathf.RoundToInt(playerPosition.x / canvasRect.rect.width * canvasTexture.width);
        int y = Mathf.RoundToInt(playerPosition.y / canvasRect.rect.height * canvasTexture.height);

        for (int i = -Mathf.FloorToInt(brushRadius); i <= Mathf.FloorToInt(brushRadius); i++)
        {
            for (int j = -Mathf.FloorToInt(brushRadius); j <= Mathf.FloorToInt(brushRadius); j++)
            {
                int pixelX = Mathf.Clamp(x + i, 0, canvasTexture.width - 1);
                int pixelY = Mathf.Clamp(y + j, 0, canvasTexture.height - 1);

                canvasTexture.SetPixel(pixelX, pixelY, Color.clear);
            }
        }
    }
}



/*

using UnityEngine;
using UnityEngine.UI;

public class MiniMapControllerPRO : MonoBehaviour
{
    public float limitationTX = 300f;
    public float limitationTY = 300f;
    public Transform player;
    float koef = 1f;
    public float mapRazmer = 34f;

    public RawImage imageToDrawOn;

    private RectTransform canvasRect;
    private Texture2D canvasTexture;
    private Vector2 lastPosition;

    void Start()
    {
        koef = limitationTX / mapRazmer;
        canvasRect = imageToDrawOn.GetComponent<RectTransform>();
        InitializeCanvasTexture();
    }

    void Update()
    {
        UpdateMiniMapPosition();
        EraseUnderPlayer();
        canvasTexture.Apply();
    }

    void UpdateMiniMapPosition()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 relativeCoordinates = rectTransform.anchoredPosition;

        Vector3 playerWorldPosition = player.position;

        float newRelativeX = Mathf.Clamp(playerWorldPosition.x * koef, 0f, limitationTX);
        float newRelativeY = Mathf.Clamp(playerWorldPosition.z * koef, 0f, limitationTY);

        Vector2 newRelativePosition = new Vector2(newRelativeX, newRelativeY);

        rectTransform.anchoredPosition = newRelativePosition;
    }

    void InitializeCanvasTexture()
    {
        canvasTexture = new Texture2D((int)canvasRect.rect.width, (int)canvasRect.rect.height);

        for (int x = 0; x < canvasTexture.width; x++)
        {
            for (int y = 0; y < canvasTexture.height; y++)
            {
                canvasTexture.SetPixel(x, y, Color.gray);
            }
        }

        imageToDrawOn.texture = canvasTexture;
        canvasTexture.Apply();
    }

    void EraseUnderPlayer()
    {
        Vector2 playerPosition = new Vector2(
            player.position.x * koef,
            player.position.z * koef
        );

        int x = Mathf.RoundToInt(playerPosition.x / canvasRect.rect.width * canvasTexture.width);
        int y = Mathf.RoundToInt(playerPosition.y / canvasRect.rect.height * canvasTexture.height);

        for (int i = -5; i <= 5; i++)
        {
            for (int j = -5; j <= 5; j++)
            {
                int pixelX = Mathf.Clamp(x + i, 0, canvasTexture.width - 1);
                int pixelY = Mathf.Clamp(y + j, 0, canvasTexture.height - 1);

                canvasTexture.SetPixel(pixelX, pixelY, Color.clear);
            }
        }
    }
}

*/