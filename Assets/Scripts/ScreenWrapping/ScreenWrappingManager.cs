using System.Collections.Generic;
using UnityEngine;

public class ScreenWrappingManager : MonoBehaviour
{
    private readonly List<Transform> m_WrapObjects = new List<Transform>();
    private Vector2 m_NegativeBorder;
    private const float k_ScreenWrapOffset = 2;

    private void Start()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            float cameraSize = mainCamera.orthographicSize;
            Vector3 cameraPosition = mainCamera.transform.position;
            m_NegativeBorder = new Vector2(cameraPosition.x - cameraSize * 2 - k_ScreenWrapOffset,
                                           cameraPosition.y - cameraSize - k_ScreenWrapOffset);
        }
    }

    private void LateUpdate()
    {
        Wrap();
    }

    public void AddToWrap(Transform transform)
    {
        m_WrapObjects.Add(transform);
    }

    public void RemoveFromWrap(Transform transform)
    {
        m_WrapObjects.Remove(transform);
    }

    private void Wrap()
    {
        foreach (Transform wrapObject in m_WrapObjects)
        {
            if (!wrapObject.gameObject.activeInHierarchy)
            {
                continue;
            }

            Vector3 position = wrapObject.position;
            Vector3 newPosition = position;
            if (position.x < m_NegativeBorder.x)
            {
                newPosition = new Vector3(m_NegativeBorder.x * -1, position.y);
            }

            if (position.x > m_NegativeBorder.x * -1)
            {
                newPosition = new Vector3(m_NegativeBorder.x, position.y);
            }

            if (position.y < m_NegativeBorder.y)
            {
                newPosition = new Vector3(position.x, m_NegativeBorder.y * -1);
            }

            if (position.y > m_NegativeBorder.y * -1)
            {
                newPosition = new Vector3(position.x, m_NegativeBorder.y);
            }

            wrapObject.position = newPosition;
        }
    }
}