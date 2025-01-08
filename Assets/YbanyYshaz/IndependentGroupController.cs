using System.Collections.Generic;
using UnityEngine;

public class IndependentGroupController : MonoBehaviour
{
    [SerializeField] private List<Transform> objects = new List<Transform>(); // Список объектов
    private Vector3[] initialOffsets; // Смещения объектов относительно центрального объекта
    private Quaternion[] initialRotations; // Вращения объектов относительно центрального объекта

    private Transform reference; // Центральный объект

    private void Start()
    {
        if (objects.Count == 0)
        {
            Debug.LogWarning("Список объектов пуст. Добавьте объекты в список.");
            return;
        }

        reference = objects[0]; // Первый объект считается центральным

        // Сохраняем начальные смещения и вращения
        initialOffsets = new Vector3[objects.Count];
        initialRotations = new Quaternion[objects.Count];

        for (int i = 0; i < objects.Count; i++)
        {
            initialOffsets[i] = reference.InverseTransformPoint(objects[i].position); // Смещение в локальных координатах
            initialRotations[i] = Quaternion.Inverse(reference.rotation) * objects[i].rotation; // Локальное вращение
        }
    }

    private void LateUpdate()
    {
        if (objects.Count == 0 || reference == null) return;

        // Обновляем позиции и вращения всех объектов
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i] == reference) continue; // Пропускаем центральный объект

            // Точное обновление позиции объекта
            Vector3 newLocalPosition = initialOffsets[i];
            objects[i].position = reference.TransformPoint(newLocalPosition);

            // Точное обновление вращения объекта
            objects[i].rotation = reference.rotation * initialRotations[i];
        }
    }
}