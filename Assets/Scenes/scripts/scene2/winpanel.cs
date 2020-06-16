using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class winpanel : view
{
   public GameObject a;
    //再来一局
    public void restart()
    {
        a.GetComponent<gamepanel>().Restart();
        
        gameObject.SetActive(false);
    }
    //返回大厅
    public void ReturnMenu()
    {
        
        SceneManager.LoadSceneAsync(0);
        
    }
}

