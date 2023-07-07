using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float health, bulletSpeed;

    bool dead = false;

    Transform muzzle;

    public Transform bullet, floatText, bloodParticle;

    public Slider slider;

    bool mouseIsNotOverUI;
    // Start is called before the first frame update
    void Start()
    {
        muzzle = transform.GetChild(1);

        slider.maxValue = health;
        slider.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        mouseIsNotOverUI = EventSystem.current.currentSelectedGameObject == null;
        // Mousenýn UI elemanlarýna týlanýp týklanmadýðýný kontrol ediyor, UI elemanýna týklanmýyorsa null ise true döndürür.
        if (Input.GetMouseButtonDown(0) && mouseIsNotOverUI)
        {
            ShootBullet();
        }
    }

    public void GetDamage(float damage)
    {
        Instantiate(floatText, transform.position, Quaternion.identity).GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
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
        if (health <= 0)
        {
            Destroy(Instantiate(bloodParticle, transform.position, Quaternion.identity),3);
            DataManager.Instance.LoseProcess();
            dead = true;
            Destroy(gameObject);
        }
    }

    void ShootBullet()
    {
        Transform tempBullet;
        tempBullet= Instantiate(bullet, muzzle.position, Quaternion.identity);
        tempBullet.GetComponent<Rigidbody2D>().AddForce(muzzle.forward * bulletSpeed);
        //forward ile Z ekseni üzerinden hareket ettirilir. Bu yüzden trasform bileþenindeki Y deðerini 90 derece ayarlanýr.

        DataManager.Instance.ShotBullet++;
    }
}
