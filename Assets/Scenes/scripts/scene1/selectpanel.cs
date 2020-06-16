using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class selectpanel : view
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //点击4x4或5x5或6x6
    public void onselectclickk(int count)
    {
        PlayerPrefs.SetInt(Const.Gamemodel, count);
        SceneManager.LoadSceneAsync(1);
    }
    
}
