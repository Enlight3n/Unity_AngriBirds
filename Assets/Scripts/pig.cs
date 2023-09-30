using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pig : MonoBehaviour
{
    public float maxspeed = 10;
    public float minspeed = 5;
    private SpriteRenderer render;
    public Sprite hurt;
    public GameObject boom;
    public GameObject Score;

    public bool isPig = false;

    public AudioClip hurtClip;
    public AudioClip dead;
    public AudioClip birdCollision;

    private void Awake() {
        render = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision) //碰撞检测，需求两物体同时挂载rigidbody和box collider
    {
        if(collision.gameObject.tag == "Player"){
            AudioPlay(birdCollision);
        }

        if(collision.relativeVelocity.magnitude > maxspeed) //直接死亡
        {
            Dead();
            
        }
        else if(collision.relativeVelocity.magnitude > minspeed) //受伤
        { 
            render.sprite =  hurt;
            AudioPlay(hurtClip);
        }
    }


    void Dead()
    {
        if(isPig)
        {
            Gamemanager._instance.pig.Remove(this);
        }
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);

        AudioPlay(dead);

        GameObject go = Instantiate(Score, transform.position + new Vector3(0,0.5f,0),Quaternion.identity);
        Destroy(go, 1.5f);

        AudioPlay(dead);
        
    }

    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
    /*
    private void OnTriggerEnter2D(Collider2D other) //只要其中一个物体挂载rigidbody和collider，但要勾选上istrigger
    {    
    }
    */
}
