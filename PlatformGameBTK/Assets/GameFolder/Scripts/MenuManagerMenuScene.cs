using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManagerMenuScene : MonoBehaviour
{
    [SerializeField]
    GameObject dataBoard;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Game");
    }
    public void DataBoardButton()
    {
        DataManager.Instance.LoadData();

        dataBoard.transform.GetChild(1).GetComponent<Text>().text = "TOTAL BULLET SHOT: " + DataManager.Instance.totalShotBullet.ToString();
        dataBoard.transform.GetChild(2).GetComponent<Text>().text = "TOTAL ENEMY KÝLLED: " + DataManager.Instance.totalEnemyKilled.ToString();
        dataBoard.SetActive(true);
    }
    public void XButton()
    {
        dataBoard.SetActive(false);
    }
}
