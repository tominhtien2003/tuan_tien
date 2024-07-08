using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public List<GameObject> features;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(2);
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

    private void Setting()
    {
        openedSetting = !openedSetting;
        features[4].SetActive(openedSetting);
    }

    private void Mail()
    {
        openedMail = !openedMail;
        features[5].SetActive(openedMail);
    }

    private void Menu()
    {
        openedMenu = !openedMenu;
        features[6].SetActive(openedMenu);
    }
}

