using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 30f; // Заданная скорость вращения

    void Update()
    {
        // Вращение объекта вокруг своей оси Y
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
