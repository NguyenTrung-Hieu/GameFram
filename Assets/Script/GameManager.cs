using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    GameObject[] enemies;
    public TextMeshProUGUI enemuCountTxt;
    //public static int LevelCount = 0;
    private const int _min = 1;
    private const int _max = 2000;
    [SerializeField]
    [Range(0, _max)]
    private static int levelCount = 0;
    public static int LevelCount
    {
        get { return levelCount; }
        set
        {
            value = Mathf.Clamp(value, _min, _max);
            if (!Mathf.Approximately(value, levelCount))
                levelCount = value;
        }
    }
    [SerializeField]
    [Range(_min, _max)]
    private int levelmax = _min;
    public int LevelMax
    {
        get { return levelmax; }
        set
        {
            value = Mathf.Clamp(value, _min, _max);
            if (!Mathf.Approximately(value, levelmax))
                levelmax = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        LevelCount++;
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("CheckEnemy", 1f);
        if(LevelCount >= LevelMax)
        {
            LevelCount = LevelMax;
        }
    }
    void Loading()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void CheckEnemy()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemuCountTxt.text = "DUNGEON "+LevelCount+" Enemies left: " + enemies.Length.ToString();
        if (enemies.Length == 0 && enemies.Length<1 && LevelCount< LevelMax)
        {
            enemuCountTxt.text = "YOU WON LOADING DUNGEON "+LevelCount;
            Invoke("Loading", 5);
        }
        else if (enemies.Length == 0 && LevelCount == LevelMax)
        {
            enemuCountTxt.text = "GAME OVER !!! THANKS FOR PLAYING !!!";
        }
    }
}

