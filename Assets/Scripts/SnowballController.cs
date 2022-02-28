using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballController : MonoBehaviour
{
    public float ballSpeed;
    public Vector3 moveDirection = Vector3.zero;
    private GameObject enemySpawner;
    private GameObject UIController;
    public AudioSource snowballHitSound;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = GameObject.Find("EnemySpawner");
        UIController = GameObject.Find("GameCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movementDirection = moveDirection * (ballSpeed * Time.deltaTime);
        transform.position += movementDirection;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemySpawner.GetComponent<EnemySpawnerController>().currentNumberOfEnemys--;
            UIController.GetComponent<GameUIController>().EnemyKilled();
            snowballHitSound.Play();
            Destroy(collision.gameObject);
        }
        //Debug.Log("hit");
        Destroy(this.gameObject);
    }

}
