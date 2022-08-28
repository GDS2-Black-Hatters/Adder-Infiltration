using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;

    public GameObject range;
    public Rigidbody bulletPrefab;

    private float shootTimer = 5;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        range = transform.Find("Range").gameObject;
        bulletPrefab = transform.Find("Bullet").gameObject.GetComponent<Rigidbody>(); ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootPlayer()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= 5.0f)
        {
            Debug.Log("bang");
            Rigidbody bullet;
            bullet = Instantiate(bulletPrefab, gameObject.transform.position, gameObject.transform.rotation);
            Vector3 target = player.transform.position - bullet.transform.position;
            bullet.velocity = target * 3;
            shootTimer = 0;
        }
    }
    
}
