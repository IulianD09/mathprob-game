using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    Transform menuPanel;
    Event keyEvent;
    Text buttonText;
    KeyCode newKey;

    bool waitingForKey;
    //public bool disableMenu;

    public GameObject optionsMenu;

    void Start()
    {
        menuPanel = transform.Find("Controls");
        menuPanel.gameObject.SetActive(false);
        waitingForKey = false;

        for (int i = 0; i < 4; i++)
        {
            if (menuPanel.GetChild(i).name == "Jump")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.jump.ToString();

            else if (menuPanel.GetChild(i).name == "Shoot")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.shoot.ToString();

            else if (menuPanel.GetChild(i).name == "Dash") 
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.dash.ToString();

            else if (menuPanel.GetChild(i).name == "Crouch")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.crouch.ToString();
        }
    }
    void Update()
    {
        if (optionsMenu.gameObject.activeSelf)
            menuPanel.gameObject.SetActive(true);
    }
    private void OnGUI()
    {
        keyEvent = Event.current;

        if(keyEvent.isKey && waitingForKey)
        {
            newKey = keyEvent.keyCode;
            waitingForKey = false;
        }
    }
    public void StartAssingment(string keyName)
    {
        if (!waitingForKey)
            StartCoroutine(AssignKey(keyName));
    }
    public void SendText(Text text)
    {
        buttonText = text;
    }
    IEnumerator WaitForKey()
    {
        while (!keyEvent.isKey)
        {
            yield return null;
        }
    }
    public IEnumerator AssignKey(string keyName)
    {
        waitingForKey = true;

        yield return WaitForKey();

        switch(keyName)
        {
            case "jump":
                GameManager.GM.jump = newKey;
                buttonText.text = GameManager.GM.jump.ToString();
                PlayerPrefs.SetString("jumpKey", GameManager.GM.jump.ToString());
                break;
            case "shoot":
                GameManager.GM.shoot = newKey;
                buttonText.text = GameManager.GM.shoot.ToString();
                PlayerPrefs.SetString("shootKey", GameManager.GM.shoot.ToString());
                break;
            case "dash":
                GameManager.GM.dash = newKey;
                buttonText.text = GameManager.GM.dash.ToString();
                PlayerPrefs.SetString("dashKey", GameManager.GM.dash.ToString());
                break;
            case "crouch":
                GameManager.GM.crouch = newKey;
                buttonText.text = GameManager.GM.crouch.ToString();
                PlayerPrefs.SetString("crouchKey", GameManager.GM.crouch.ToString());
                break;
        }
        yield return null;
    }
}
