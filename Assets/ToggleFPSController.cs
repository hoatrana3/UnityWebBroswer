using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ToggleFPSController : MonoBehaviour
{
    private FirstPersonController m_FirstPersonController;
    private GameObject m_FirstPersonCharacter;
    private Vector3 m_StartPosition, m_DriftPosition;
    private Quaternion m_StartRotation, m_DriftRotation, m_DriftCharacterRotation;

    private bool m_IsEnabled = false;
    private float m_DriftSeconds = 0.75F;
    private float m_DriftTimer = 0;
    private bool m_IsDrifting = false;

    void Start()
    {
        m_FirstPersonController = GetComponent<FirstPersonController>();
        m_FirstPersonCharacter = transform.GetChild(0).gameObject;
        m_StartPosition = transform.position;
        m_StartRotation = new Quaternion(0,0,0,1);
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Q))
        {
            m_IsEnabled = !m_IsEnabled;
            StartDrift();
        }

        m_FirstPersonController.enabled = m_IsEnabled;
        m_FirstPersonController.GetMouseLook().SetCursorLock(m_IsEnabled);

        if (m_IsDrifting)
        {
            m_DriftTimer += Time.deltaTime;
            
            if (m_DriftTimer > m_DriftSeconds)
            {
                StopDrift();
            } else
            {
                float ratio = m_DriftTimer / m_DriftSeconds;
                transform.position = Vector3.Lerp(m_DriftPosition, m_StartPosition, ratio);
                transform.rotation = Quaternion.Slerp(m_DriftRotation, m_StartRotation, ratio);
                m_FirstPersonCharacter.transform.rotation = Quaternion.Slerp(m_DriftCharacterRotation, m_StartRotation, ratio);
            }
        }
    }

    private void StartDrift()
    {
        m_IsDrifting = true;
        m_DriftTimer = 0;
        m_DriftPosition = transform.position;
        m_DriftRotation = transform.rotation;
        m_DriftCharacterRotation = m_FirstPersonCharacter.transform.rotation;
    }

    private void StopDrift()
    {
        m_IsDrifting = false;
        transform.position = m_StartPosition;
        transform.rotation = m_StartRotation;
    }
}
