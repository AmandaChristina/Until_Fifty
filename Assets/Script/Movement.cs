﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Vector2 movementInput;

    private Vector3 direction;
    public Tilemap fogOfWar;
    bool hasMoved;

    void Start()
    {

    }

    void Update()
    {
        if (movementInput.x == 0)
        {
            hasMoved = false;
        }

         else if (movementInput.x != 0 && !hasMoved)
        {
            hasMoved = true;
            GetMovementDirection();
        }
    }

    public void GetMovementDirection()
    {
        if (movementInput.x < 0)
        {
            if (movementInput.y > 0)
            {
                direction = new Vector3(-0.5f, 0.5f);
            }

            else if (movementInput.y < 0)
            {
                direction = new Vector3(-0.5f, -0.5f);
            }

            else
            {
                direction = new Vector3(-1, 0, 0);
            }

            transform.position += direction;
            UpdateFogOfWar();
        }

        else if (movementInput.x > 0)
        {
            if (movementInput.y > 0)
            {
                direction = new Vector3(0.5f, 0.5f);
            }
            else if (movementInput.y < 0)
            {
                direction = new Vector3(0.5f, -0.5f);
            }
            else
            {
                direction = new Vector3(1, 0, 0);
            }

            transform.position += direction;
            UpdateFogOfWar();
        }

       

    }


    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    public void MeuMove()
    {
        if (Input.GetAxis("Horizontal"))
        {

        } 
        movementInput = transform.position + 
    }

    public int vision = 1;

    void UpdateFogOfWar()
    {
        Vector3Int currentPlayerTile = fogOfWar.WorldToCell(transform.position);

        for(int x =-vision; x<= vision; x++)
        {
            for(int y=-vision; y<= vision; y++)
            {
                fogOfWar.SetTile(currentPlayerTile + new Vector3Int(x, y, 0), null);
            }
        }
    }


}
