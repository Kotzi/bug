using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour 
{
    private GameObject plant;

    private float plantTimer = 0f;
    private bool canPickUpPlant = true;

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
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public bool hasPlant()
    {
        return (plant != null);
    }
}