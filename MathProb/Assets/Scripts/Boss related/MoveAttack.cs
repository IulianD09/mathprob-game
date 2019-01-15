using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAttack : MonoBehaviour
{
    [Header("Min Y move range")]
    [SerializeField] private Transform minY;

    [Header("Max Y move range")]
    [SerializeField] private Transform maxY;

    [Space (3)]

    Vector3 direction;

    public float m_Speed;
    public float waitTime;
    public bool allowAttack = false;

    private void Start()
    {
        direction = Vector3.up;
        InvokeRepeating("Movement", 3f, 10f);
    }

    IEnumerator Movement()
    {
        if(allowAttack)
        {
            yield return new WaitForSecondsRealtime(waitTime);

            if (direction == Vector3.down)
            {
                m_Speed = 5f;

                direction = Vector3.up;

                yield return new WaitForSeconds(waitTime);

                transform.position = maxY.position;

                transform.Translate(direction * m_Speed * Time.deltaTime);
            }
            else if (direction == Vector3.up)
            {
                m_Speed = 5f;

                direction = Vector3.down;

                yield return new WaitForSeconds(waitTime);

                transform.position = minY.position;

                transform.Translate(direction * m_Speed * Time.deltaTime);
            }
        }
    }
}
