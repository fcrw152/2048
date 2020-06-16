using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class gamepanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        initgrid();
        creatnumber();
    }
    public Dictionary<int, int> girdsize = new Dictionary<int, int>() { { 4, 180 }, { 5, 140 }, { 6, 120 } };
    public Text score;
    public Text maxscore;
    public Button laststep;
    public Button restart;
    public Button exit;
    public Transform gridpartent;
    public void onlaststepclick() { }
    public void onstartclick() { }
    public void onexitclick() { }
    public GameObject Itemprefab;
    public GameObject numberprefab;
    public Mygrid[][] grids = null;
    private Vector3 Downposition, Upposition;//用于存放鼠标抬起或者鼠标按下的位置
    int girdnum;
    public List<Mygrid> cancreatgrid = new List<Mygrid>();//可以方数字的格子
    public void initgrid()
    {
        girdnum = PlayerPrefs.GetInt(Const.Gamemodel, 4);
        grids = new Mygrid[girdnum][];
        GridLayoutGroup gridLayoutGroup= gridpartent.GetComponent<GridLayoutGroup>();
        gridLayoutGroup.constraintCount = girdnum;
        gridLayoutGroup.cellSize = new Vector2(girdsize[girdnum], girdsize[girdnum]);
        for (int i = 0; i< girdnum;i++)
        {
            for (int j = 0; j < girdnum; j++)
            {
                if (grids[i] == null) { grids[i] = new Mygrid[girdnum]; }
                grids[i][j]=creatgrid();
            }
        }

    }//初始化格子
    public Mygrid creatgrid()
    {
       GameObject gameObject=  GameObject.Instantiate(Itemprefab, gridpartent);
        return gameObject.GetComponent<Mygrid>();//?
        
    }
    public void creatnumber()
    {
        cancreatgrid.Clear();
        for (int i = 0; i < girdnum; i++)
        {
            for (int j = 0; j < girdnum; j++)
            {
                if (!grids[i][j].ishavenumber()) { cancreatgrid.Add(grids[i][j]); }
            }
        }
        if (cancreatgrid.Count == 0) { return; }
        //找到数字的格子
        //随机一个格子
        int index = Random.Range(0, cancreatgrid.Count);
       GameObject gameobj= GameObject.Instantiate(numberprefab, cancreatgrid[index].transform);
        gameobj.GetComponent<Mynumber>().init(cancreatgrid[index]);//?
        //创建数字
    }
    public void onpointdown()
    {
        //Debug.Log(Input.mousePosition);
        Downposition = Input.mousePosition;

    }
    public void onpointup()
    {
        Upposition = Input.mousePosition;
        if (Vector3.Distance(Downposition, Upposition) < 30) { return ; }
        movetype movedir = moveDir();
        Debug.Log(movedir);
        Movenumber(movedir);
        creatnumber();
        ResetNumberStatus();
        Isgamelose();
    }
    public movetype moveDir()
    {
         if (Mathf.Abs(Upposition.x - Downposition.x) >= Mathf.Abs(Upposition.y - Downposition.y))
        {
            //x轴绝对值大于y轴绝对值，左右移动。
            if (Upposition.x - Downposition.x > 0)
            {  
                return movetype.RIGHT;
            }
            else
            {   
                return movetype.LEFT;
            }
        }
        else
        {
            if (Upposition.y - Downposition.y > 0)
            {  
                return movetype.UP;
            }
            else
            {    
                return movetype.DOWN;
            }
        }
    }
    public void Movenumber(movetype movedir)
    {
        switch (movedir)
        {
            case movetype.UP:

                for (int j = 0; j < girdnum; j++)
                {
                    for (int i = 1; i < girdnum ; i++)
                    {
                        if (grids[i][j].ishavenumber())
                        {
                            Mynumber mynumber = grids[i][j].GetNumber();
                            for (int m = i - 1; m >= 0; m--)
                            {
                                if (grids[m][j].ishavenumber())
                                {
                                    //判断是否合并
                                    if (mynumber.Getnumber() == grids[m][j].GetNumber().Getnumber() && grids[m][j].GetNumber().status == numberstatus.Canmath)
                                    {
                                        grids[m][j].GetNumber().mach();
                                        mynumber.Getmygrid().SetNumber(null);
                                        Destroy(mynumber.gameObject);
                                    }
                                    break;
                                }
                                else
                                {
                                    //没有数字直接上移；
                                    mynumber.Movenumber(grids[m][j]);
                                }
                            }
                        }
                    }
                }

                break;
            case movetype.DOWN:
                for (int j = 0; j < girdnum; j++)
                {
                    for (int i = girdnum-1; i >=0; i--)
                    {
                        if (grids[i][j].ishavenumber())
                        {
                            Mynumber mynumber = grids[i][j].GetNumber();
                            for (int m = i + 1; m <girdnum; m++)
                            {
                                if (grids[m][j].ishavenumber())
                                {
                                    //判断是否合并
                                    if (mynumber.Getnumber() == grids[m][j].GetNumber().Getnumber() && grids[m][j].GetNumber().status == numberstatus.Canmath)
                                    {
                                        grids[m][j].GetNumber().mach();
                                        mynumber.Getmygrid().SetNumber(null);
                                        Destroy(mynumber.gameObject);
                                    }
                                    break;
                                }
                                else
                                {
                                    //没有数字直接上移；
                                    mynumber.Movenumber(grids[m][j]);
                                }
                            }
                        }
                    }
                }
                break;
            case movetype.LEFT:

                for (int i = 0; i < girdnum; i++)
                {
                    for (int j = 1; j < girdnum; j++)
                    {
                        if (grids[i][j].ishavenumber())
                        {
                            Mynumber mynumber = grids[i][j].GetNumber();
                            for (int m = j - 1; m >= 0; m--)
                            {
                                if (grids[i][m].ishavenumber())
                                {
                                    //判断是否合并
                                    if (mynumber.Getnumber() == grids[i][m].GetNumber().Getnumber() && grids[i][m].GetNumber().status == numberstatus.Canmath)
                                    {
                                        grids[i][m].GetNumber().mach();
                                        mynumber.Getmygrid().SetNumber(null);
                                        Destroy(mynumber.gameObject);
                                    }
                                    break;
                                }
                                else
                                {
                                    //没有数字直接上移；
                                    mynumber.Movenumber(grids[i][m]);
                                }
                            }
                        }
                    }
                }

                break;
            case movetype.RIGHT:

                for (int i = 0; i < girdnum; i++)
                {
                    for (int j = girdnum - 1; j >= 0; j--)
                    {
                        if (grids[i][j].ishavenumber())
                        {
                            Mynumber mynumber = grids[i][j].GetNumber();
                            for (int m = j + 1; m < girdnum; m++)
                            {
                                if (grids[i][m].ishavenumber())
                                {
                                    //判断是否合并
                                    if (mynumber.Getnumber() == grids[i][m].GetNumber().Getnumber()&& grids[i][m].GetNumber().status==numberstatus.Canmath)
                                    {
                                        grids[i][m].GetNumber().mach();
                                        mynumber.Getmygrid().SetNumber(null);
                                        Destroy(mynumber.gameObject);
                                    }
                                    break;
                                }
                                else
                                {
                                    //没有数字直接上移；
                                    mynumber.Movenumber(grids[i][m]);
                                }
                            }
                        }
                    }
                }
                break;
        }
    }
    public void ResetNumberStatus()
    {
        for (int i = 0; i < girdnum; i++)
        {
            for (int j = 0; j < girdnum; j++)
            {
                if (grids[i][j].ishavenumber())
                {
grids[i][j].GetNumber().status = numberstatus.Canmath;
                }
                
                
            }
        }
    }
    public void Exit()
    {
        SceneManager.LoadSceneAsync(0);
    }
   public GameObject win;
    public void gamewin()
    {
        win.SetActive(true);
        Debug.Log("WIN");
    }
    public void Isgamelose()
    {
        int step = 0;

        for (int i = 0; i < girdnum; i++)
        {
            for (int j = 0; j < girdnum; j++)
            {
                if (grids[i][j].ishavenumber())
                {
                    step++;
                }
            }
        }
        if (step == girdnum * girdnum) { Debug.Log("LOSE"); GameObject.Find("Canvas/lose").SetActive(true); }
        
    }
    public void Restart()
    {
        for (int i = 0; i < girdnum; i++)
        {
            for (int j = 0; j < girdnum; j++)
            {
                if (grids[i][j].ishavenumber())
                {
                    Destroy(grids[i][j].GetNumber().gameObject);
                    grids[i][j].SetNumber(null);
                }
            }
        }
    }
}
