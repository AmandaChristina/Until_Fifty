using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Vector2 movementInput;
    private Vector3 direction;
    public Tilemap fogOfWar;
    public Tilemap world;
    public Tile tile;
    //public Vector3Int location;
    public Vector3Int location;
    bool hasMoved;

    [SerializeField]
    int lances = 50;
    int lancesMax = 50;

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

            if (lances > 0){
                GetMovementDirection();
                lances--;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            GET();
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

    void OnTriggerEnter2D(Collider2D objeto)
    {
        if(objeto.gameObject.layer > gameObject.layer)
        {
            print("Player: "+ gameObject.layer+", Objeto:" + objeto.gameObject.layer );
            lances -= 2;
            gameObject.layer = objeto.gameObject.layer;
        }

        else if(objeto.gameObject.layer <= gameObject.layer)
        {
            gameObject.layer = objeto.gameObject.layer;
        }

        else if(objeto.gameObject.tag == "Pedra")
        {
            lances -= 2;
        }
    }
    
    void GET()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        location = world.WorldToCell(mousePosition);
        Vector3 novaPostionPlayer = world.CellToWorld(location);
        if (world.GetTile(location))
        {
            
            print(world.CellToWorld(location));
            transform.position = novaPostionPlayer;
        }
        else print("não tem");
    }

    void OnMouseOver()
    {
        
    }
}
