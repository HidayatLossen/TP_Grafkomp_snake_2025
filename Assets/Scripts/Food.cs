using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;


    public void Start()
    {
        RandomizePosition();
    }

    // private void RandomizePosition()
    // {
    //     Bounds bounds = gridArea.bounds;

    //     float x = Random.Range(bounds.min.x, bounds.max.x);
    //     float y = Random.Range(bounds.min.y, bounds.max.y);
    //     transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    // }

private void RandomizePosition()
    {
        Bounds bounds = gridArea.bounds;

        Vector3 newPosition;

        bool positionValid;
        do
        {
            float x = Mathf.Round(Random.Range(bounds.min.x, bounds.max.x));
            float y = Mathf.Round(Random.Range(bounds.min.y, bounds.max.y));
            newPosition = new Vector3(x, y, 0.0f);

            positionValid = true;

            // Cek apakah posisi ini nabrak tubuh snake
            GameObject[] snakeSegments = GameObject.FindGameObjectsWithTag("Snake");

            foreach (GameObject segment in snakeSegments)
            {
                if (segment.transform.position == newPosition)
                {
                    positionValid = false;
                    break;
                }
            }

        } while (!positionValid);

        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Snake"))
        {
            RandomizePosition();
        }
    }

}
