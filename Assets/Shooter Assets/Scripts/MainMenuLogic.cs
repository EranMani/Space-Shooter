using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    [SerializeField]
    private string m_firstLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("CurrentLives", 3);
        PlayerPrefs.SetInt("CurrentScore", 0);

        SceneManager.LoadScene(m_firstLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
