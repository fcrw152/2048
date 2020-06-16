using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mygrid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Mynumber number;

    public bool ishavenumber()//格子是否有数字
    {
        return number!=null;
    }
    public Mynumber GetNumber()//获取格子里的数字
    {
        return number;
    }
    public void SetNumber(Mynumber mynumber)//设置数字
    {
        number = mynumber;
    }
}
