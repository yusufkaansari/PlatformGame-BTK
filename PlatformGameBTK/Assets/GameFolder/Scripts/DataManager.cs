using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TigerForge;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    private int shotBullet;
    public int totalShotBullet;
    private int enemyKilled;
    public int totalEnemyKilled;

    EasyFileSave myFile;
    void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            StartProcess();
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int ShotBullet
    {
        get 
        { 
            return shotBullet; 
        }
        set 
        { 
            shotBullet = value;
            if (GameObject.Find("ShotBulletText")!= null)
            {
                GameObject.Find("ShotBulletText").GetComponent<Text>().text = "SHOT BULLET : " + shotBullet.ToString();
            }
        }
    }

    public int EnemyKilled
    {
        get 
        { 
            return enemyKilled; 
        }
        set 
        { 
            enemyKilled = value;
            if (GameObject.Find("EnemyKilledText") != null)
            {
                GameObject.Find("EnemyKilledText").GetComponent<Text>().text = "KILLED ENEMY : " + enemyKilled.ToString();
                WinProcess();
            }
        }
    }
    void StartProcess()
    {
        myFile = new EasyFileSave();
        LoadData();
    }

    public void SaveData()
    {
        totalShotBullet += shotBullet;
        totalEnemyKilled += enemyKilled;

        myFile.Add("totalShotBullet", totalShotBullet);
        myFile.Add("totalEnemyKilled", totalEnemyKilled);

        myFile.Save();
    }
    void ResetAllData()
    {
        myFile.Add("totalShotBullet", 0);
        myFile.Add("totalEnemyKilled", 0);

        myFile.Save();
    }

    public void LoadData()
    {
        if (myFile.Load())
        {
            totalShotBullet = myFile.GetInt("totalShotBullet");
            totalEnemyKilled = myFile.GetInt("totalEnemyKilled");
        }
    }

    public void WinProcess()
    {
        if (enemyKilled >= 5)
        {
            SaveData();
            ResetScore();
            SceneManager.LoadScene("Win");
            //print("Kazandýnýz !!");
        }
    }
    public void LoseProcess()
    {
        StartCoroutine(LoseProcessCo());
    }
    IEnumerator LoseProcessCo()
    {

    // 1 saniye bekle
    yield return new WaitForSeconds(1f);
        SaveData();
        ResetScore();
        SceneManager.LoadScene("GameOver");

    }
    public void ResetScore()
    {       
        shotBullet = 0;
        enemyKilled = 0;
    }
}
