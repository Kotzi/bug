using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    private GameController gameController;
    private GameObject plant;

    private float plantTimer = 0f;
    private bool canPickUpPlant = true;

    void Start()
    {
        gameController = Object.FindObjectOfType<GameController>();
    }

    void Update()
    {
        if (!canPickUpPlant)
        {
            plantTimer -= Time.deltaTime;
            if (plantTimer <= 0)
            {
                canPickUpPlant = true;
            }
        }
    }
    
    public void doAction()
    {
        if (plant)
        {
            canPickUpPlant = false;
            plantTimer = 5f;
            plant.transform.parent = transform.parent;
            plant.GetComponent<Rigidbody2D>().isKinematic = false;
            plant.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            plant.GetComponent<Collider2D>().enabled = true;
            plant.GetComponent<PlantController>().plantDropped();
            plant = null;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.name == "Plant" && canPickUpPlant) 
        {
            col.collider.enabled = false;
            col.collider.GetComponent<Rigidbody2D>().isKinematic = true;
            col.collider.transform.parent = transform;
            plant = col.collider.gameObject;
        }
        else if (col.collider.name.Contains("Ant"))
        {
            gameController.restartGame();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Zone2PlayerDetector")
        {
            gameController.onZone2Detected();
        } 
        else if (other.name == "Zone3PlayerDetector")
        {
            gameController.onZone3Detected();
        }
        else if (other.name == "Zone4PlayerDetector")
        {
            gameController.onZone4Detected();
        }
    }

    public bool hasPlant()
    {
        return (plant != null);
    }
}