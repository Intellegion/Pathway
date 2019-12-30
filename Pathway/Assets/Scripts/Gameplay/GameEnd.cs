using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameEnd : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private TMP_Text result;
    public static bool outcome;
    public static int score;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = score.ToString();
        result.text = outcome==true?"Victory":"Defeat";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void End(bool o, int s)
    {
        outcome = o;
        score = s;
    }
    public void Retry()
    {
        SceneManager.LoadScene(0);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
