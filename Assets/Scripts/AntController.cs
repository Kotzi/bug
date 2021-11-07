using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntController : MonoBehaviour
{
    public bool shouldBeOnGround = true;
    public bool isStopped = false;
    public Route[] groundRoutes;
    public Route[] ceilRoutes;

    private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite antSprite;

    [SerializeField] private Sprite antWithPlantSprite;

    private int routeToGo;

    private float tParam;

    private Vector2 objectPosition;

    private float speedModifier;

    private bool coroutineAllowed;
    private bool hasPlant = false;

    void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        speedModifier = 0.5f;
        coroutineAllowed = true;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (coroutineAllowed && !isStopped)
        {
            StartCoroutine(goByTheRoute(routeToGo));
        }

        if (hasPlant)
        {
            spriteRenderer.sprite = antWithPlantSprite;
        }
        else
        {
            spriteRenderer.sprite = antSprite;
        }
    }

    void OnBecameInvisible()
    {
        if (hasPlant)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator goByTheRoute(int routeNum)
    {
        coroutineAllowed = false;

        var routes = shouldBeOnGround ? groundRoutes : ceilRoutes;
        Vector2 p0 = routes[routeNum].controlPoints[hasPlant ? 3 : 0].position;
        Vector2 p1 = routes[routeNum].controlPoints[hasPlant ? 2 : 1].position;
        Vector2 p2 = routes[routeNum].controlPoints[hasPlant ? 1 : 2].position;
        Vector2 p3 = routes[routeNum].controlPoints[hasPlant ? 0 : 3].position;

        while(tParam < 1)
        {
            if(!isStopped)
            {
                tParam += Time.deltaTime * speedModifier;

                objectPosition = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;

                transform.position = objectPosition;
                yield return new WaitForEndOfFrame();
            }
            else
            {
                coroutineAllowed = true;
                yield return new WaitForEndOfFrame();
            }
        }

        tParam = 0f;

        if (hasPlant)
        {
            routeToGo -= 1;
        }
        else
        {
            routeToGo += 1;
        }

        if(routeToGo > routes.Length - 1)
        {
            hasPlant = true;
            routeToGo -= 1;
        }
        else if (routeToGo < 0)
        {
            hasPlant = false;
            routeToGo = 0;
        }

        coroutineAllowed = true;
    }
}
