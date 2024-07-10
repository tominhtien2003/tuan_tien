using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager_tuan : MonoBehaviour
{
    public static UiManager_tuan instance;
    private bool openedShop;
    private bool openedProfile;
    private bool openedTask;
    private bool openedBag;
    private bool openedSetting;
    private bool openedMail;
    private bool openedMenu;
    private bool choosedMap;
    private bool chooseTank;

    public Color mixColor;
    public List<GameObject> map;
    public List<GameObject> features;

    public string url = "https://www.youtube.com/";
    public string urlFace = "https://www.facebook.com/";
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Shop()
    {
        openedShop = !openedShop;
        features[0].SetActive(openedShop);
    }

    public void Profile()
    {
        openedProfile = !openedProfile;
        features[1].SetActive(openedProfile);
    }

    public void Task()
    {
        openedTask = !openedTask;
        features[2].SetActive(openedTask);
    }

    public void Bag()
    {
        openedBag = !openedBag;
        features[3].SetActive(openedBag);
    }

    public void Setting()
    {
        openedSetting = !openedSetting;
        features[4].SetActive(openedSetting);
    }

    public void Mail()
    {
        openedMail = !openedMail;
        features[5].SetActive(openedMail);
    }

    public void Menu()
    {
        openedMenu = !openedMenu;
        features[6].SetActive(openedMenu);
    }

    public void Tank()
    {
        chooseTank = !chooseTank;
        features[7].SetActive(chooseTank);
    }

    public void Youtube()
    {
        Application.OpenURL(url);
    }
    public void FaceBook()
    {
        Application.OpenURL(urlFace);
    }
    public void ChooseMap()
    {
        choosedMap = !choosedMap;
        if (choosedMap)
        {
            map[0].GetComponent<Image>().color = mixColor;
            map[1].GetComponent<Image>().color = Color.white;
        }
        else
        {
            map[0].GetComponent<Image>().color = Color.white;
            map[1].GetComponent<Image>().color = mixColor;
        }
    }
}

