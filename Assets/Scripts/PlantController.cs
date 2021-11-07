using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour 
{
    [SerializeField] private LayerMask plantDetectorLayerMask;
    private GameController gameController;
    private bool isInGroundArea = true;

    void Awake()
    {
        gameController = Object.FindObjectOfType<GameController>();
    }
	
    void OnTriggerEnter2D(Collider2D other)
    {
        processArea(other);
    }

    public void plantDropped()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f, plantDetectorLayerMask);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				processArea(colliders[i]);
			}
		}
    }

    void processArea(Collider2D other)
    {
        if (other.name == "GroundRouteArea")
        {
            if (!isInGroundArea)
            {
                isInGroundArea = true;
                gameController.changeAntsRoute(isInGroundArea);
            }
        }
        else if (other.name == "CeilRouteArea")
        {
            if(isInGroundArea)
            {
                isInGroundArea = false;
                gameController.changeAntsRoute(isInGroundArea);
            }
        }
    }
}