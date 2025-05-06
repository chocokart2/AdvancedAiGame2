using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direction = Vector3.zero;
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float lifeTime = 3.0f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemyComponent = other.gameObject.GetComponent<Enemy>();
        if (enemyComponent != null)
        {
            enemyComponent.Hit();
        }
        PlayerController playerComponent = other.gameObject.GetComponent<PlayerController>();
        if (playerComponent != null)
        {
            playerComponent.Hit();
        }
        Drone droneComponent = other.gameObject.GetComponent<Drone>();
        if (droneComponent != null)
        {
            droneComponent.Hit();
        }
    }
}
