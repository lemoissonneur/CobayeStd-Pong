using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseScreenManager : MonoBehaviour
{
    [SerializeField]
    private Text winnerText = null;
    [SerializeField]
    private Text scoreText = null;


    private static PauseScreenManager instance;
    public static PauseScreenManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        IsActive(false);
    }
    
    void Start()
    {
        
    }

    public void Victory(string winner, int scorePong, int scorePing)
    {
        winnerText.text = winner;
        scoreText.text = scorePing.ToString() + " / " + scorePong.ToString();
        IsActive(true);
    }

    public void IsActive(bool isActive)
    {
        this.gameObject.SetActive(isActive);
    }

    public void NewGameBtn()
    {
        GameManager.Instance.NewGame();
        IsActive(false);
    }

    public void MainMenuBtn()
    {
        SceneManager.LoadScene("Accueil");
    }


}
