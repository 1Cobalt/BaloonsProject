using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{

    public float speed = 0.1f;
    public int hp = 1;

    private Vector3 localSpeed;

    [SerializeField] private ParticleSystem destroyParticles;
    [SerializeField] private AudioSource popSound;

    public void Start()
    {
        localSpeed = new Vector3(0.0f, 0.0f, speed);
        //localSpeed = new Vector3(0.0f, 0.0f, 0.0f);
    }

    public void Damage(int damage)
    {
        hp = hp - damage;
        switch (this.gameObject.tag)
        {
            case "HealBall":
                {
                    ParticleSystem temp = Instantiate(destroyParticles, transform.position, Quaternion.identity);
                    temp.transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
                    Camera.main.gameObject.GetComponent<GameController>().DamagePlayer(-1);
                    Destroy(temp, 4.0f);
                    Destroy(this.gameObject);

                    break;
                }

            case "IgnoreBall":
                {
                    ParticleSystem temp = Instantiate(destroyParticles, transform.position, Quaternion.identity);
                    temp.transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
                    Camera.main.gameObject.GetComponent<GameController>().DamagePlayer(1);
                    Destroy(temp, 4.0f);
                    Destroy(this.gameObject);

                    break;
                }

            case "DurableBall":
                {
                    ParticleSystem temp = Instantiate(destroyParticles, transform.position, Quaternion.identity);
                    this.GetComponent<Renderer>().material.color = Color.grey;
                    temp.transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
                    Destroy(temp, 4.0f);
                    this.gameObject.tag = "NormalBall";

                    break;
                }
            case "MiniBossBall":
                {
                    ParticleSystem temp = Instantiate(destroyParticles, transform.position, Quaternion.identity);
                    temp.transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
                    this.GetComponent<Renderer>().material.color = new Color(this.GetComponent<Renderer>().material.color.r + 0.1f, this.GetComponent<Renderer>().material.color.g + 0.1f, this.GetComponent<Renderer>().material.color.b + 0.1f);
                    Destroy(temp, 4.0f);

                    break;
                }
            case "BossBall":
                {
                    ParticleSystem temp = Instantiate(destroyParticles, transform.position, Quaternion.identity);
                    temp.transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
                    this.GetComponent<Renderer>().material.color = new Color(this.GetComponent<Renderer>().material.color.r + 0.01f, this.GetComponent<Renderer>().material.color.g + 0.01f, this.GetComponent<Renderer>().material.color.b + 0.01f);
                    Destroy(temp, 4.0f);

                    break;
                }

            default:
                {
                    ParticleSystem temp = Instantiate(destroyParticles, transform.position, Quaternion.identity);
                    temp.transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
                    temp.startColor = this.GetComponent<Renderer>().material.color;
                    Destroy(temp, 4.0f);
     
                    break;
                }
        }
        if (hp <= 0)
        {
            if (this.gameObject.tag == "BossBall")
            {
                Camera.main.gameObject.GetComponent<GameController>().FinishLevel();
                StaticDataStorage.BossIsDestroyed();
            }
           
            StaticDataStorage.BaloonDestroyed();

            GetComponent<Renderer>().enabled = false; 
            GetComponent<Collider>().enabled = false; 
            popSound.Play();
            Destroy(gameObject, popSound.clip.length);
        }
    }

    void Update()
    { 
        this.transform.Translate(localSpeed * Time.deltaTime);
    }
}
