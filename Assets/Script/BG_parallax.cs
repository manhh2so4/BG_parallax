using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_parallax : MonoBehaviour
{
    public Camera cam;
    [SerializeField] private float[] parallaxEffect_X = new float[4];
    [SerializeField] private float[] parallaxEffect_Y = new float[4];
    Vector2[] mPosition = new Vector2[4];
    public int BG_type;
    float height;
    float width;
    float Field1Y,Field2Y,houseY,mountainY;
    public Sprite[] imgBg;
    public Transform[] Bg0 = new Transform[3];
    public Transform[] Bg1 = new Transform[3];
    public Transform[] Bg2 = new Transform[3];
    public Transform[] Bg3 = new Transform[3];

    [SerializeField] GameObject prefab;
    private void Reset() {
        cam = Camera.main;
        LoadComponent();
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;        
        LoadImgBG();
    }
    private void Awake(){
        cam = Camera.main;
        Debug.Log("cam.transform.position.y" + cam.transform.position.y);
        LoadComponent();
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect; 
        Debug.Log(height + " hsjdfaj " + width);       
        LoadImgBG();
        for (int i = 0; i < 4; i++)
        {
            mPosition[i] = new Vector2(transform.GetChild(i).position.x,transform.GetChild(i).position.y);
        }
        
    }
    private void Update() {
        ParallaxEff();
    }
    void ParallaxEff(){
        for (int i = 0; i < 4; i++)
        {
            float distanceX = cam.transform.position.x * parallaxEffect_X[i];
            float distanceY = cam.transform.position.y * parallaxEffect_Y[i];
            transform.GetChild(i).position = new Vector3(mPosition[i].x+distanceX,distanceY+mPosition[i].y);
        }

        
    }
    void LoadComponent(){
        Bg0[0] = transform.GetChild(0).GetChild(0);
        Bg0[1] = transform.GetChild(0).GetChild(1);
        Bg0[2] = transform.GetChild(0).GetChild(2);

        Bg1[0] = transform.GetChild(1).GetChild(0);
        Bg1[1] = transform.GetChild(1).GetChild(1);
        Bg1[2] = transform.GetChild(1).GetChild(2);

        Bg2[0] = transform.GetChild(2).GetChild(0);
        Bg2[1] = transform.GetChild(2).GetChild(1);
        Bg2[2] = transform.GetChild(2).GetChild(2);

        Bg3[0] = transform.GetChild(3).GetChild(0);
        Bg3[1] = transform.GetChild(3).GetChild(1);
        Bg3[2] = transform.GetChild(3).GetChild(2);
    }
    void LoadImgBG(){
        Field1Y = 0 ;
        Field2Y = (float)imgBg[0].texture.height/100;
        houseY = Field2Y +  (float)imgBg[1].texture.height/100;
        mountainY = Field2Y +  (float)imgBg[1].texture.height/100 +0.4f;

        LoadBG(ref Bg0[0],24,0,0);
        LoadBG(ref Bg0[1],24,1,0);
        LoadBG(ref Bg0[2],24,2,0);

        LoadBG(ref Bg1[0],24,0,1);
        LoadBG(ref Bg1[1],24,1,1);
        LoadBG(ref Bg1[2],24,2,1);

        LoadBG(ref Bg2[0],192,0,2);
        LoadBG(ref Bg2[1],192,1,2);
        LoadBG(ref Bg2[2],192,2,2);

        LoadBG(ref Bg3[0],64,0,3);
        LoadBG(ref Bg3[1],64,1,3);
        LoadBG(ref Bg3[2],64,2,3);
    }
    void LoadBG(ref Transform obj,int size,int index,int idImg){
        int loop = (int)width*100/(size*4);
        float hSet = (cam.transform.position.y - height/2) ;        
        switch (idImg)
            {
                case 0:
                hSet +=  (float)imgBg[idImg].texture.height/200;
                break;
                case 1:
                hSet +=  Field2Y + (float)imgBg[idImg].texture.height/200;
                break;
                case 2:
                hSet +=  houseY + (float)imgBg[idImg].texture.height/200;
                break;
                case 3:
                hSet +=  mountainY + (float)imgBg[idImg].texture.height/200;
                break;
            }
        for (int i = 0; i < loop; i ++)
        {
            GameObject temp;
            try
            {
                temp = obj.GetChild(i).gameObject;
            }
            catch (System.Exception)
            {                                
                temp = Instantiate(prefab, obj);               
            } 
            temp.GetComponent<SpriteRenderer>().sprite = imgBg[idImg];
            temp.GetComponent<SpriteRenderer>().sortingOrder = -2-idImg;
            temp.name = imgBg[idImg].name;
            temp.transform.position = new Vector3((cam.transform.position.x-(float)(loop*size)/50 )+((float)(i*size*4)/100)+(float)loop*size*(1-index)/25,
                                                    hSet ,0);
        }
    }
}
