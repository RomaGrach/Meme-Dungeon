using UnityEngine;
using UnityEngine.UI;

public class DrawingScript : MonoBehaviour
{
    public RawImage imageToDrawOn;
    public float brushSize = 10f;
    public Color brushColor = Color.black;

    private RectTransform canvasRect;
    private Texture2D canvasTexture;
    private Vector2 lastPosition;

    void Start()
    {
        canvasRect = GetComponent<RectTransform>();
        InitializeCanvasTexture();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 localPosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, null, out localPosition))
            {
                DrawOnTexture(localPosition);
            }
        }
        else
        {
            lastPosition = Vector2.zero; // Сброс lastPosition при отпускании кнопки мыши
        }
    }

    void InitializeCanvasTexture()
    {
        canvasTexture = new Texture2D((int)canvasRect.rect.width, (int)canvasRect.rect.height);

        // Заполнение всей текстуры серым цветом при старте
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

    void DrawOnTexture(Vector2 position)
    {
        int x = Mathf.RoundToInt(position.x / canvasRect.rect.width * canvasTexture.width);
        int y = Mathf.RoundToInt(position.y / canvasRect.rect.height * canvasTexture.height);

        if (lastPosition == Vector2.zero)
        {
            lastPosition = position;
        }

        DrawLine(lastPosition, position);

        lastPosition = position;

        canvasTexture.Apply();
    }

    void DrawLine(Vector2 startPos, Vector2 endPos)
    {
        int steps = Mathf.CeilToInt(Vector2.Distance(startPos, endPos) / brushSize);
        for (int i = 0; i <= steps; i++)
        {
            float t = i / (float)steps;
            int x = Mathf.RoundToInt(Mathf.Lerp(startPos.x, endPos.x, t));
            int y = Mathf.RoundToInt(Mathf.Lerp(startPos.y, endPos.y, t));
            DrawPixel(x, y);
        }
    }

    void DrawPixel(int x, int y)
    {
        for (int i = -Mathf.FloorToInt(brushSize / 2); i < Mathf.CeilToInt(brushSize / 2); i++)
        {
            for (int j = -Mathf.FloorToInt(brushSize / 2); j < Mathf.CeilToInt(brushSize / 2); j++)
            {
                int pixelX = Mathf.Clamp(x + i, 0, canvasTexture.width - 1);
                int pixelY = Mathf.Clamp(y + j, 0, canvasTexture.height - 1);

                // Изменение цвета на Color.clear для стирания
                canvasTexture.SetPixel(pixelX, pixelY, Color.clear);
            }
        }
    }
}



/*
using UnityEngine;
using UnityEngine.UI;

public class DrawingScript : MonoBehaviour
{
    public RawImage imageToDrawOn;
    public float brushSize = 10f;
    public Color brushColor = Color.black;

    private RectTransform canvasRect;
    private Texture2D canvasTexture;
    private Vector2 lastPosition;

    void Start()
    {
        canvasRect = GetComponent<RectTransform>();
        InitializeCanvasTexture();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 localPosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, null, out localPosition))
            {
                DrawOnTexture(localPosition);
            }
        }
        else
        {
            lastPosition = Vector2.zero; // Сброс lastPosition при отпускании кнопки мыши
        }
    }

    void InitializeCanvasTexture()
    {
        canvasTexture = new Texture2D((int)canvasRect.rect.width, (int)canvasRect.rect.height);
        imageToDrawOn.texture = canvasTexture;
        ClearCanvas();
    }

    void DrawOnTexture(Vector2 position)
    {
        int x = Mathf.RoundToInt(position.x / canvasRect.rect.width * canvasTexture.width);
        int y = Mathf.RoundToInt(position.y / canvasRect.rect.height * canvasTexture.height);

        if (lastPosition == Vector2.zero)
        {
            lastPosition = position;
        }

        DrawLine(lastPosition, position);

        lastPosition = position;

        canvasTexture.Apply();
    }

    void DrawLine(Vector2 startPos, Vector2 endPos)
    {
        int steps = Mathf.CeilToInt(Vector2.Distance(startPos, endPos) / brushSize);
        for (int i = 0; i <= steps; i++)
        {
            float t = i / (float)steps;
            int x = Mathf.RoundToInt(Mathf.Lerp(startPos.x, endPos.x, t));
            int y = Mathf.RoundToInt(Mathf.Lerp(startPos.y, endPos.y, t));
            DrawPixel(x, y);
        }
    }

    void DrawPixel(int x, int y)
    {
        for (int i = -Mathf.FloorToInt(brushSize / 2); i < Mathf.CeilToInt(brushSize / 2); i++)
        {
            for (int j = -Mathf.FloorToInt(brushSize / 2); j < Mathf.CeilToInt(brushSize / 2); j++)
            {
                int pixelX = Mathf.Clamp(x + i, 0, canvasTexture.width - 1);
                int pixelY = Mathf.Clamp(y + j, 0, canvasTexture.height - 1);
                canvasTexture.SetPixel(pixelX, pixelY, brushColor);
            }
        }
    }

    public void ClearCanvas()
    {
        for (int x = 0; x < canvasTexture.width; x++)
        {
            for (int y = 0; y < canvasTexture.height; y++)
            {
                canvasTexture.SetPixel(x, y, Color.clear);
            }
        }
        canvasTexture.Apply();
    }
}
*/