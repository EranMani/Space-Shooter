using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCompleteLogic : MonoBehaviour
{
    [SerializeField]
    private float m_timeBetweenText;

    [SerializeField]
    private bool m_canExit;

    [SerializeField]
    private string m_toMainMenu = "MainMenu";

    [SerializeField]
    private Text m_message, m_score, m_pressKey;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowTextCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (m_canExit && Input.anyKeyDown)
        {
            SceneManager.LoadScene(m_toMainMenu);
        }
    }

    private IEnumerator ShowTextCoroutine()
    {
        yield return new WaitForSeconds(m_timeBetweenText);
        m_message.gameObject.SetActive(true);

        yield return new WaitForSeconds(m_timeBetweenText);
        m_score.gameObject.SetActive(true);
        m_score.text = "Final Score: " + PlayerPrefs.GetInt("CurrentScore");

        yield return new WaitForSeconds(m_timeBetweenText);
        m_pressKey.gameObject.SetActive(true);
        m_canExit = true;
    }
}
