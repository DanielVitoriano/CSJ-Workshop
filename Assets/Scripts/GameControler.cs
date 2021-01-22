using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControler : MonoBehaviour
{
    public GameObject GameOverPanel;
    public Text textScore;
    int score;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        GameOverPanel.SetActive(false);
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > 2){
            increseScore(1);
            time = 0;
        }
    }

    public void ShowGameOver(){
        GameOverPanel.SetActive(true);
    }

    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void increseScore(int value){
        score += value;
        textScore.text = "SCORE: " + score;
    }
}
