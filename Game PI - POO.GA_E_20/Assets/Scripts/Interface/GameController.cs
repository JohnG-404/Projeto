using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject gameOver;
    public static GameController instance;
    public Vector2 lastCheckPointPos;
    public Animator transition;
    public float transitionTime = 3f;
    public static bool GameIsPaused = false;
    public GameObject PMenuUI;

    void Awake(){
        
        Time.timeScale = 1;
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }
    public void Resume(){
        PMenuUI.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
    }
    void Pause(){
        PMenuUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void ShowGameOver()
    {
        int lifeP = PlayerPI_M.currentHealth;
        if(lifeP <= 0){
            gameOver.SetActive(true);
            Time.timeScale = 0;
        }
        
    }
    public void RestartGame(string lvlName)
    {
        SceneManager.LoadScene(lvlName);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex){
        
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex); 
    }

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.gameObject.CompareTag("Player")){
            LoadNextScene();
        }
    }
    
}
