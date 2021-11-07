using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntsNestController : MonoBehaviour
{
    private const float MAX_ANT_SPAWN_COOLDOWN = 0.5f;

    [SerializeField] private Route[] groundRoutes;

    [SerializeField] private Route[] ceilRoutes;

    [SerializeField] private GameObject antPrefab;

    public bool active = false;

    private float antSpawnCooldown = 0f;
    private bool antsStopped = false;
    private bool antsShouldBeOnGround = true;

    void Update()
    {
        if (active)
        {
            antSpawnCooldown -= Time.deltaTime;
            if (antSpawnCooldown <= 0f && !antsStopped)
            {
                antSpawnCooldown = MAX_ANT_SPAWN_COOLDOWN;
                var ant = Instantiate(antPrefab, transform.position, transform.rotation, transform).GetComponent<AntController>();
                ant.groundRoutes = groundRoutes;
                ant.ceilRoutes = ceilRoutes;
                ant.shouldBeOnGround = antsShouldBeOnGround;
                ant.isStopped = antsStopped;
            }
        }
    }

    public void changeAntsRoute(bool shouldBeOnGround)
    {
        if (shouldBeOnGround != antsShouldBeOnGround)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<AntController>().shouldBeOnGround = shouldBeOnGround;
            }
            antsShouldBeOnGround = shouldBeOnGround;
        }
    }

    public void changeAntsMode(bool stopped)
    {
        if (stopped != antsStopped)
        {
            antsStopped = stopped;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<AntController>().isStopped = stopped;
            }
        }
    }
}
