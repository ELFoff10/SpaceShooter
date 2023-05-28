using UnityEngine;
using UnityEngine.EventSystems;

public class PointerClickHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool m_Hold;
    public bool Hold => m_Hold;

    public void OnPointerDown(PointerEventData eventData)
    {
        m_Hold = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_Hold = false;
    }
}
