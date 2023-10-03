using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public float health, damage;

    bool colliderBusy = false;

    public Slider slider;

    AudioSource dieSound;
    bool Isdie = false;

    [SerializeField]
    Transform firstPos, secondPos;
    public float speed;
    Vector3 nextPos;

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = health;
        slider.value = health;

        dieSound = GetComponent<AudioSource>();

        nextPos = firstPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == firstPos.position)
            nextPos = secondPos.position;
        if (transform.position == firstPos.position)
            nextPos = secondPos.position;
        if (transform.position == secondPos.position)
            nextPos = firstPos.position;
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // collision yerine "other" degisken ismi
        if (other.CompareTag("Player") && !colliderBusy)
        {
            colliderBusy = true;
            other.GetComponent<PlayerManager>().GetDamage(damage);
        }
        else if (other.CompareTag("Bullet"))
        {
            GetDamage(other.GetComponent<BulletManager>().bulletDamage);
            Destroy(other.gameObject);
        }        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            colliderBusy = false;
        }
    }
    public void GetDamage(float damage)
    {
        if (health - damage >= 0)
        {
            health -= damage;
        }
        else
        {
            health = 0;
        }
        slider.value = health;
        AmIDead();
    }

    void AmIDead()
    {
        if (health <= 0 && !Isdie)
        {
            Isdie = true;
            dieSound.Play();
            DataManager.Instance.EnemyKilled++;
            Destroy(gameObject,0.333f);            
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(firstPos.position, secondPos.position);
    }
}
