using System.Collections;
using UnityEngine;

public class Drone : MonoBehaviour, IShot
{
    [SerializeField] Transform pivot;
    [SerializeField] float moveTermMin = 1;
    [SerializeField] float moveTermMax = 2;
    [SerializeField] float moveSpeedMin = 1;
    [SerializeField] float moveSpeedMax = 2;

    float moveTerm;
    Vector3 directionUnitVector;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Move();
    }

    void Move()
    {
        IEnumerator m_MoveRoutine()
        {
            while (true)
            {
                // 렌덤 워크 기반 무작위 움직임 구현
                float angle = Random.Range(0f, Mathf.PI * 2);
                float moveSpeed = Random.Range(moveSpeedMin, moveSpeedMax);
                moveTerm = Random.Range(moveTermMin, moveTermMax);
                directionUnitVector = new Vector3(
                    Mathf.Sin(angle), 0, Mathf.Cos(angle));

                float endTime = Time.time + moveTerm;
                while (Time.time < endTime)
                {
                    transform.Translate(directionUnitVector * moveSpeed * Time.deltaTime);
                    yield return null;
                }

            }
        }

        StartCoroutine(m_MoveRoutine());
    }


    public void Hit()
    {
        Destroy(gameObject);
        GameManager.instance.score++;
    }
}
