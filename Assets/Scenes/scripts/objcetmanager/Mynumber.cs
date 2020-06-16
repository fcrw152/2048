using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Mynumber : MonoBehaviour
{
    public Image bgimage;
    public Text numtext;
    public Mygrid grid;
    public numberstatus status ;
    public Color[] colors;
    public List<int> number_index;
    private void Awake()
    {
        bgimage = gameObject.GetComponent<Image>();
        numtext = transform.Find("Text").GetComponent<Text>();
    }
    public void init(Mygrid mygrid)
    {
        mygrid.SetNumber(this);
        SetGrid(mygrid);
        givenumber(2);
        status = numberstatus.Canmath;
        transform.localScale = Vector3.zero;
    }
    public void SetGrid(Mygrid mygrid)
    {
        this.grid = mygrid;
    }
    public void givenumber(int number)
    {
        this.numtext.text = number.ToString();
        this.bgimage.color = colors[number_index.IndexOf(number)];
    }
    public int Getnumber()
    {
        return int.Parse(numtext.text);
    }
    public Mygrid Getmygrid()
    {
        return this.grid;
    }
    public void Movenumber (Mygrid mygrid)
    {
        transform.SetParent(mygrid.transform);
        transform.localPosition = Vector3.zero;
        this.Getmygrid().SetNumber(null);
        mygrid.SetNumber(this);
        SetGrid(mygrid);
    }
    public void mach()
    {
        int num = this.Getnumber() * 2;
        if (num == 2048)
        {
            GameObject.Find("Canvas/gamepanel").GetComponent<gamepanel>().gamewin();
        }
        this.givenumber(this.Getnumber() * 2);
        status = numberstatus.unmath;
    }
    private float TIME;
    private void Update()
    {
        if (this.Getnumber ()> 100&& this.Getnumber() <1000) { numtext.fontSize = 85; }
        if (this.Getnumber() > 1000) { numtext.fontSize = 60; }
        TIME += Time.deltaTime;
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, TIME*3);
    }
}
