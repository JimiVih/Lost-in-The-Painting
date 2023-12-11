using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver_script : MonoBehaviour
{
    public Transform GameOver, click, panel;
    bool canClick;

    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        if (canClick)
        {
            if (Input.GetMouseButton(0))
            {
                StartCoroutine(StartAgain());
            }
        }
    }

    public void GameOVER()
    {
        StartCoroutine(GameOverTEXT());
    }

    IEnumerator StartAgain()
    {
        panel.gameObject.SetActive(true);
        GameOver.GetComponent<Animator>().SetBool("FadeIn", false);
        panel.GetComponent<Animator>().SetBool("fade", true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator GameOverTEXT()
    {
        GameOver.gameObject.SetActive(true);
        GameOver.GetComponent<Animator>().SetBool("FadeIn", true);
        yield return new WaitForSeconds(5);
        click.gameObject.SetActive(true);
        canClick = true;
    }
}
