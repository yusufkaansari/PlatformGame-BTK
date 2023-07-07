using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float health, bulletSpeed;

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
        // Mousen�n UI elemanlar�na t�lan�p t�klanmad���n� kontrol ediyor, UI eleman�na t�klanm�yorsa null ise true d�nd�r�r.
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
            Destroy(gameObject);
        }
    }

    void ShootBullet()
    {
        Transform tempBullet;
        tempBullet= Instantiate(bullet, muzzle.position, Quaternion.identity);
        tempBullet.GetComponent<Rigidbody2D>().AddForce(muzzle.right * bulletSpeed);
        //muzzle.forward ile Z ekseni (Blue axis) �zerinden hareket ettirilir. Bu y�zden trasform bile�enindeki Y de�erini 90 derece ayarlan�r.
        //muzzle.right ile X ekseni (Red axis)
        //muzzle.up ile X ekseni (Green axis)

        DataManager.Instance.ShotBullet++;
    }
}
