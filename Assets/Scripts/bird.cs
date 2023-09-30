using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird : MonoBehaviour
{
    private bool isClick = false;
    public Transform rightPos;
    public float maxDis = 3;
    [HideInInspector]
    public SpringJoint2D sp;
    private Rigidbody2D rg;


    public LineRenderer right;
    public LineRenderer left;
    public Transform leftPos;

    public GameObject boom;

    private bool canmove = true;

    public TestMyTrail myTrail;

    public float smooth = 3;

    public AudioClip select;
    public AudioClip fly;

    private void Awake() 
    {
        sp = GetComponent<SpringJoint2D>();
        rg = GetComponent<Rigidbody2D>();
        myTrail = GetComponent<TestMyTrail>();
    }


    private void OnMouseDown() //鼠标按下
    {
        if(canmove)
        {
            AudioPlay(select);
            isClick = true;
            rg.isKinematic = true; //。可使用脚本来移动运动刚体对象（通过修改对象的变换组件），但该对象不会像非运动刚体一样响应碰撞和力。
        }
    }
    private void OnMouseUp() //鼠标抬起
    {
        if(canmove){
        isClick = false;
        rg.isKinematic = false;
        Invoke("Fly",0.1f); //经过多长时间后，调用该方法一次,让spjoint在0.1s后失活
        
        //禁用划线组件
        right.enabled = false;
        left.enabled = false;
        canmove = false;
        }
    }


    private void Update()
    {
        if(isClick)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //transform.position += new Vector3(0,0,10);
            transform.position += new Vector3(0,0,-Camera.main.transform.position.z);

            if (Vector3.Distance(transform.position, rightPos.position) > maxDis)
            {
                Vector3 pos = (transform.position - rightPos.position).normalized;//单位化向量
                pos *= maxDis; //限定最大长度

                transform.position = pos +rightPos.position;
            }
            Line();
        }

        //相机跟随

        float posX = transform.position.x;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Mathf.Clamp(posX, 0, 15), 
        Camera.main.transform.position.y,Camera.main.transform.position.z),smooth*Time.deltaTime);
    }
    void Fly(){
        AudioPlay(fly);
        sp.enabled = false;
        Invoke("Next",5);
        myTrail.StartTrails();
    }

    void Line()
    {
        right.enabled = true;
        left.enabled = true;

        right.SetPosition(0 , rightPos.position);
        right.SetPosition(1, transform.position);

        left.SetPosition(0, leftPos.position);
        left.SetPosition(1, transform.position);
    }
    
    void Next()
    {
        Gamemanager._instance.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        Gamemanager._instance.NextBird();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        myTrail.ClearTrails();
    } 

    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
    
}
