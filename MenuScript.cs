using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "StartMenu") {
            menu = transform.Find("Menu").gameObject;
            menu.SetActive(false);

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (menu && Input.GetKeyDown(KeyCode.Escape)) {
            Time.timeScale = 0;
            menu.SetActive(true);

        }
    }


    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void Resume() {

        Time.timeScale = 1;
        menu.SetActive(false);
    }

    
}
