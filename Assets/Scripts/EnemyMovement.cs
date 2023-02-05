using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    float xMaxBound, xMinBound, zMaxBound, zMinBound;

    int randomPatrol, direction, randomDirection;

    Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        xMaxBound = 5.5f;
        xMinBound = -8.5f;
        zMaxBound = 1.5f;
        zMinBound = -7.5f;

        randomPatrol = Random.Range(1, 3);
        randomDirection = Random.Range(1, 3);


        if (randomDirection == 1)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
    }

    public void preEnemyMovement()
    {
        StartCoroutine(EnemyMove(0.3f));
    }

    // Update is called once per frame
    public IEnumerator EnemyMove(float duration)
    {
       

        float time = 0;

        //Izquierda derecha
        if (randomPatrol == 1)
        {
            destination = transform.localPosition + new Vector3(direction, 0, 0);

            if (destination.x > xMaxBound || destination.x < xMinBound || Physics.Raycast(transform.position, new Vector3(direction, 0, 0), 1.1f))
            {
                if (direction == 1)
                {
                    direction = -1;
                }
                else
                {
                    direction = 1;
                }
            }

            destination = transform.position + new Vector3(direction, 0, 0);

            while (time < duration)
            {
                transform.position = Vector3.Lerp(transform.position, destination, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
        }

        //Arriba abajo
        else if(randomPatrol == 2)
        {
            destination = transform.position + new Vector3(0, 0, direction);

            Collider[] sphereColliders = Physics.OverlapSphere(destination, 0.1f); 

            if (destination.z > zMaxBound || destination.z < zMinBound || sphereColliders.Length != 0)
            {
                if (direction == 1)
                {
                    direction = -1;
                }
                else
                {
                    direction = 1;
                }
            }

            destination = transform.position + new Vector3(0, 0, direction);

            while (time < duration)
            {
                transform.position = Vector3.Lerp(transform.position, destination, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
        }
    }
}
