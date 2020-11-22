using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleWebBrowser;

public class KeyboardFeedback : MonoBehaviour
{
    private KeyboardSoundHandler soundHandler;
    public bool keyHit = false;
    public bool keyCanBeHitAgain = false;

    private float originYPosition;
    private GameObject textObject;

    private WebBrowser parentWebBrowser;
    void Start()
    {
        soundHandler = GameObject.FindGameObjectWithTag("KeyboardSoundHandler").GetComponent<KeyboardSoundHandler>();
        originYPosition = transform.position.y;
        textObject = transform.GetChild(0).gameObject;

        if (parentWebBrowser == null)
            parentWebBrowser = GameObject.FindGameObjectWithTag("WebBrowserMain").GetComponent<WebBrowser>();
    }
    void Update()
    {
        if (keyHit)
        {
            soundHandler.PlayKeyClick();
            keyCanBeHitAgain = false;
            keyHit = false;
            transform.position += new Vector3(0, -0.05f, 0);
            handleKeyPressed();
        }

        if (transform.position.y < originYPosition)
        {
            transform.position += new Vector3(0, 0.01f, 0);
        } else
        {
            keyCanBeHitAgain = true;
        }
    }

    private void OnMouseDown()
    {
        keyHit = true;
    }

    private void handleKeyPressed()
    {
        string currentText = textObject.GetComponent<TextMeshPro>().text;
        if (currentText == "ENTER")
        {
            parentWebBrowser.OnNavigate();
            return;
        }

        string targetText = parentWebBrowser.mainUIPanel.UrlField.text;
        switch (currentText)
        {
            case "DEL":
                targetText = targetText.Substring(0, targetText.Length - 1);
                break;
            case "CLR":
                targetText = "";
                break;
            default:
                targetText += currentText;
                break;
        }

        parentWebBrowser.mainUIPanel.UrlField.text = targetText;
    }
}
