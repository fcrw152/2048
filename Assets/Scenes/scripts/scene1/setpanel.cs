using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class setpanel : view
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Slider slider_sound;
    //关闭界面
    public void oncloseclick()
    {
        hide();
    }
    //调整音量大小
    public void onsoundchange(float f) 
    {
        PlayerPrefs.SetFloat(Const.Sound, f);
    }
    public override void show()
    {
        base.show();
        slider_sound.value = PlayerPrefs.GetFloat(Const.Sound);
    }
}
