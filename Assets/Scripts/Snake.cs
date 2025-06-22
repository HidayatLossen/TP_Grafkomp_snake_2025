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

    public void Start()
    {
        // _segments = new List<Transform>();
        // _segments.Add(transform);

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

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && _direction != Vector2.down)
        {
            _inputDirection = Vector2.up;
        }
        else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && _direction != Vector2.up)
        {
            _inputDirection = Vector2.down;
        }
        else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && _direction != Vector2.right)
        {
            _inputDirection = Vector2.left;
        }
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && _direction != Vector2.left)
        {
            _inputDirection = Vector2.right;
        }
    }

    private void FixedUpdate()
    {
        // Terapkan arah input ke arah utama hanya di FixedUpdate (agar sinkron)
        _direction = _inputDirection; // Tambahan ============

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

    public void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
    }

    private void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(transform);

        for (int i = 1; i < initialSize; i++)
        {
            // _segments.Add(Instantiate(segmentPrefab, transform.position, Quaternion.identity));
            _segments.Add(Instantiate(segmentPrefab));
        }

        transform.position = Vector3.zero;
        _direction = Vector2.right; // Tambahan ============
        _inputDirection = Vector2.right; // Tambahan ============
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            Grow();
        }
        else if (other.CompareTag("Obstacle"))
        {
            ResetState();
        }
    }

}
