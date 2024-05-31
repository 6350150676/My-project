using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    RectTransform m_rtBack;
    RectTransform m_rtJoystick;

    public Transform player;  // Reference to the player

    float m_Radius;
    float m_Speed = 5.0f;

    Vector3 m_VecMove;
    bool m_bTouch = false;

    void Start()
    {
        m_rtBack = transform.Find("JoystickBack").GetComponent<RectTransform>();
        m_rtJoystick = transform.Find("JoystickBack/Joystick").GetComponent<RectTransform>();

        // Ensure the player reference is set
        if (player == null)
        {
            Debug.LogError("Player Transform is not assigned in the JoystickController script.");
        }

        m_Radius = m_rtBack.rect.width * 0.5f;
    }

    void Update()
    {
        if (m_bTouch && player != null)
        {
            player.position += m_VecMove;
        }
    }

    void OnTouch(Vector2 vecTouch)
    {
        Vector2 vec = new Vector2(vecTouch.x - m_rtBack.position.x, vecTouch.y - m_rtBack.position.y);

        vec = Vector2.ClampMagnitude(vec, m_Radius);
        m_rtJoystick.localPosition = vec;
        float fSqr = (m_rtBack.position - m_rtJoystick.position).sqrMagnitude / (m_Radius * m_Radius);

        Vector2 vecNormal = vec.normalized;

        m_VecMove = new Vector3(vecNormal.x * m_Speed * Time.deltaTime * fSqr, 0f, vecNormal.y * m_Speed * Time.deltaTime * fSqr);
        if (player != null)
        {
            player.eulerAngles = new Vector3(0f, Mathf.Atan2(vecNormal.x, vecNormal.y) * Mathf.Rad2Deg, 0f);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnTouch(eventData.position);
        m_bTouch = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnTouch(eventData.position);
        m_bTouch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // return to the original position
        m_rtJoystick.localPosition = Vector3.zero;
        m_bTouch = false;
    }
}
