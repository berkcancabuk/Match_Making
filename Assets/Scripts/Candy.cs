﻿using DG.Tweening;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Candy : Tile
{
    Vector2 _endPos;
    Vector2 _startPos;
    private bool _draggingStarted;

    private const float swipeThreshold = 0.2f;

    Direction _direction = Direction.None;

    Vector3 _mousePosition;

    public TextMeshProUGUI text;


    public CandyType GetCandyType()
    {
        return candyType;
    }

    private void OnMouseDown()
    {
        if (Camera.main != null) _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("On begin Drag");
        _draggingStarted = true;
        _startPos = _mousePosition;
        print(arrayPos);
        Board.Instance.selectedObject = arrayPos;
    }

    private void OnMouseDrag()
    {
        
        if (_draggingStarted)
        {
            if (Camera.main != null) _endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 difference = _endPos - _startPos;

            if (difference.magnitude > swipeThreshold)
            {
                if (Mathf.Abs(difference.x) > Mathf.Abs(difference.y))
                {
                    _direction = difference.x > 0
                        ? Direction.Right
                        : Direction.Left;
                }
                else
                {
                    _direction = difference.y > 0
                        ? Direction.Up
                        : Direction.Down;
                }
            }
            else
            {
                _direction = Direction.None;
            }
            print(_direction);
        }
    }

    private void OnMouseUp()
    {
        _startPos = Vector2.zero;
        _endPos = Vector2.zero;
        _draggingStarted = false;
      
        Board.Instance.TileSwapCheck(Board.Instance.selectedObject, _direction);
    }
}