using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController_tuan : MonoBehaviour
{
    public static GameController_tuan instance;

    public GameObject Chart;
    public GameObject ChatPage;
    public GameObject Leave;
    public Transform Barrel;

    private bool CheckedChart;
    private bool CheckedChat;
    private bool CheckedLeave;

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

    public void GetBullet(Transform spawnPoint, GameObject bulletPrefab)
    {
        GameObject newBullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
        newBullet.transform.rotation = spawnPoint.rotation;
        StartCoroutine(WaitForDestroy(newBullet));
    }

    IEnumerator WaitForDestroy(GameObject bullet)
    {
        yield return new WaitForSeconds(10f);
        Destroy(bullet);
    }

    public void ViewChart()
    {
        CheckedChart = !CheckedChart;
        Chart.SetActive(CheckedChart);
    }

    public void Chat()
    {
        CheckedChat = !CheckedChat;
        ChatPage.SetActive(CheckedChat);
    }

    public void BoxExit()
    {
        CheckedLeave = !CheckedLeave;
        Leave.SetActive(CheckedLeave);
    }

    public void ExitGame()
    {
        Debug.Log("Exited");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(2);
    }
}
