using System;
using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private Vector2 _inputDirection = Vector2.right; // Tambahan ============
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;

    public int initialSize = 4;

    private Queue<Transform> _segmentPool = new Queue<Transform>(); // Tambahan Versi 2

    public Food food;



    public ScoreManager scoreManager;

    public AudioClip eatSound;
    public AudioClip hitSound;

    private AudioSource audioSource;



    private float moveTimer = 0f;
    private float moveDelay = 0.05f; // Awal = 0.07 detik
    private float minDelay = 0.01f;  // Batas bawah (maksimum kecepatan)
    private float speedIncrease = 0.002f; // Setiap kali makan, delay dikurangi 0.002




    public void Start()
    {
        // _segments = new List<Transform>();
        // _segments.Add(transform);
        audioSource = GetComponent<AudioSource>();
        ResetState();
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        // {
        //     _direction = Vector2.up;
        // }
        // else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        // {
        //     _direction = Vector2.down;
        // }
        // else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        // {
        //     _direction = Vector2.left;
        // }
        // else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        // {
        //     _direction = Vector2.right;
        // }

        // if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && _direction != Vector2.down)
        // {
        //     _inputDirection = Vector2.up;
        // }
        // else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && _direction != Vector2.up)
        // {
        //     _inputDirection = Vector2.down;
        // }
        // else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && _direction != Vector2.right)
        // {
        //     _inputDirection = Vector2.left;
        // }
        // else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && _direction != Vector2.left)
        // {
        //     _inputDirection = Vector2.right;
        // }
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && _direction != Vector2.down)
        {
            _inputDirection = Vector2.up;
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && _direction != Vector2.up)
        {
            _inputDirection = Vector2.down;
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && _direction != Vector2.right)
        {
            _inputDirection = Vector2.left;
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && _direction != Vector2.left)
        {
            _inputDirection = Vector2.right;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }




        // kecepatan snake:
        // ========== Timer Gerak ==========
        moveTimer += Time.deltaTime;
        if (moveTimer >= moveDelay)
        {
            Move();
            moveTimer = 0f;
        }
    }

    private void Move()
    {
        _direction = _inputDirection;

        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        transform.position = new Vector3(
            Mathf.Round(transform.position.x) + _direction.x,
            Mathf.Round(transform.position.y) + _direction.y,
            0.0f
        );
    }

    // private void FixedUpdate()
    // {
    //     // Terapkan arah input ke arah utama hanya di FixedUpdate (agar sinkron)
    //     _direction = _inputDirection; // Tambahan ============

    //     for (int i = _segments.Count - 1; i > 0; i--)
    //     {
    //         _segments[i].position = _segments[i - 1].position;
    //     }

    //     transform.position = new Vector3(
    //         Mathf.Round(transform.position.x) + _direction.x,
    //         Mathf.Round(transform.position.y) + _direction.y,
    //         0.0f
    //     );
    // }

    public void Grow()
    {
        // Transform segment = Instantiate(segmentPrefab);
        // segment.position = _segments[_segments.Count - 1].position;
        // _segments.Add(segment);


        // Tambahan Versi 2
        Transform segment;

        if (_segmentPool.Count > 0)
        {
            segment = _segmentPool.Dequeue();
            segment.gameObject.SetActive(true);
        }
        else
        {
            segment = Instantiate(segmentPrefab);
        }

        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
    }

    private void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            // Destroy(_segments[i].gameObject);

            // Tambahan Versi 2
            _segments[i].gameObject.SetActive(false);
            _segmentPool.Enqueue(_segments[i]);
        }

        _segments.Clear();
        _segments.Add(transform);

        for (int i = 1; i < initialSize; i++)
        {
            // _segments.Add(Instantiate(segmentPrefab, transform.position, Quaternion.identity));
            // _segments.Add(Instantiate(segmentPrefab));

            // Tambahan Versi 2
            Grow();
        }

        transform.position = Vector3.zero;
        // _direction = Vector2.right; // Tambahan ============
        // _inputDirection = Vector2.right; // Tambahan ============
        // transform.rotation = Quaternion.Euler(0, 0, 0);

        // Random direction
        float random = UnityEngine.Random.value;
        if (random < 0.25f)
        {
            _direction = Vector2.right;
            _inputDirection = Vector2.right;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (random < 0.5f)
        {
            _direction = Vector2.left;
            _inputDirection = Vector2.left;
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (random < 0.75f)
        {
            _direction = Vector2.up;
            _inputDirection = Vector2.up;
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else
        {
            _direction = Vector2.down;
            _inputDirection = Vector2.down;
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }

        // Reset kecepatan ke default
        moveDelay = 0.05f;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            Grow();

            // Tambah kecepatan (kurangi delay), tapi tidak lebih cepat dari minDelay
            moveDelay = Mathf.Max(minDelay, moveDelay - speedIncrease);

            if (scoreManager != null)
            {
                scoreManager.AddScore(1);
            }

            if (eatSound != null)
            {
                audioSource.PlayOneShot(eatSound);
            }
        }
        else if (other.CompareTag("Obstacle"))
        {
            if (hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
        
            ResetState();
            food.SendMessage("RandomizePosition");
            if (scoreManager != null)
            {
                scoreManager.ResetScore();
            }
            
            
        }
    }

}
