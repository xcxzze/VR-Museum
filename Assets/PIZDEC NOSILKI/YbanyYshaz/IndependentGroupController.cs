using System.Collections.Generic;
using UnityEngine;

public class IndependentGroupController : MonoBehaviour
{
    [SerializeField] private List<Transform> objects = new List<Transform>(); // ������ ��������
    private Vector3[] initialOffsets; // �������� �������� ������������ ������������ �������
    private Quaternion[] initialRotations; // �������� �������� ������������ ������������ �������

    private Transform reference; // ����������� ������

    private void Start()
    {
        if (objects.Count == 0)
        {
            Debug.LogWarning("������ �������� ����. �������� ������� � ������.");
            return;
        }

        reference = objects[0]; // ������ ������ ��������� �����������

        // ��������� ��������� �������� � ��������
        initialOffsets = new Vector3[objects.Count];
        initialRotations = new Quaternion[objects.Count];

        for (int i = 0; i < objects.Count; i++)
        {
            initialOffsets[i] = reference.InverseTransformPoint(objects[i].position); // �������� � ��������� �����������
            initialRotations[i] = Quaternion.Inverse(reference.rotation) * objects[i].rotation; // ��������� ��������
        }
    }

    private void LateUpdate()
    {
        if (objects.Count == 0 || reference == null) return;

        // ��������� ������� � �������� ���� ��������
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i] == reference) continue; // ���������� ����������� ������

            // ������ ���������� ������� �������
            Vector3 newLocalPosition = initialOffsets[i];
            objects[i].position = reference.TransformPoint(newLocalPosition);

            // ������ ���������� �������� �������
            objects[i].rotation = reference.rotation * initialRotations[i];
        }
    }
}