using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ClickMovement : MonoBehaviour
{
    public Tilemap fogOfWar;
    public Tilemap world;
    public GameObject tile;
    public Tile tileMouseOver;
    Camera cam;


    public Vector3Int location;
    Vector3 worldCelltoWorld;
    Vector3 novaPostionPlayer;
    Vector3 mousePosition;

    [SerializeField]
    public int lances = 50;
    int lancesMax = 50;
    [SerializeField]
    public int custoTotal;
    public string textTileMap;
    public bool isVisible;

    void Start()
    {
        cam = gameObject.GetComponentInChildren<Camera>();
    }

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        location = world.WorldToCell(mousePosition);
        //novaPostionPlayer = world.CellToWorld(location)
        worldCelltoWorld = world.CellToWorld(location);
        tile.transform.position = worldCelltoWorld;

        if (Input.GetMouseButtonDown(0)){
            limitadorMethod(); 
        }
        Raycast2DMethod();
        verificacaoMouseOver();
    }
    void Raycast2DMethod()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit){

            if (hit.transform.gameObject.layer == 8) textTileMap = "Piso 1";
            if (hit.transform.gameObject.layer == 9) textTileMap = "Piso 2";
            if (hit.transform.gameObject.layer == 10) textTileMap = "Piso 3";
            verificacaoCustoMethod(hit.transform.gameObject.layer, gameObject.layer);
            
        }
    }
    void verificacaoCustoMethod(int layerTile, int layerPlayer)
    {
        if (Input.GetMouseButton(0)) gameObject.layer = layerTile;

        if (layerTile > layerPlayer){
            custosMethod(2);
            
        }
        else{
            custosMethod(1);
        }

    }
    void MovementClickMethod()
    {
        if(lances > 0f) { 
            if (world.GetTile(location)){
                //print(worldCelltoWorld);
                novaPostionPlayer = Vector3.Lerp(transform.position, worldCelltoWorld, 24);
                transform.position = novaPostionPlayer;
                UpdateFogOfWar();
            }
            lances -= custoTotal;
        }
    }
    void custosMethod(int custoLance)
    {
        custoTotal = custoLance;
    }
    void limitadorMethod()
    {
        Vector3Int tileAtual = world.WorldToCell(transform.position);
        Vector3 tileAtualToWorld = world.CellToWorld(tileAtual);
        float movementHorizontal = worldCelltoWorld.x - tileAtualToWorld.x;
        float movementVertical = worldCelltoWorld.y - tileAtualToWorld.y;

        if (movementHorizontal > 0.5f && movementVertical > 0.5f
            || movementHorizontal < -0.5f && movementVertical > 0.5f
            || movementHorizontal > 0.5f && movementVertical < -0.5f
            || movementHorizontal < -0.5f && movementVertical < -0.5f
            || movementHorizontal > 1f || movementHorizontal < -1f
            || movementVertical > 0.5f || movementVertical < -0.5f
            //|| movementVertical == 0f && movementVertical == 0f
            ){
            isVisible = false;
        }
        else{
            MovementClickMethod();
            
        }
    }

    void verificacaoMouseOver()
    {
        Vector3Int tileAtual = world.WorldToCell(transform.position);
        Vector3 tileAtualToWorld = world.CellToWorld(tileAtual);
        float movementHorizontal = worldCelltoWorld.x - tileAtualToWorld.x;
        float movementVertical = worldCelltoWorld.y - tileAtualToWorld.y;

        if (movementHorizontal > 0.5f && movementVertical > 0.5f
            || movementHorizontal < -0.5f && movementVertical > 0.5f
            || movementHorizontal > 0.5f && movementVertical < -0.5f
            || movementHorizontal < -0.5f && movementVertical < -0.5f
            || movementHorizontal > 1f || movementHorizontal < -1f
            || movementVertical > 0.5f || movementVertical < -0.5f
            //|| movementVertical == 0f && movementVertical == 0f
            )
        {
            isVisible = false;
        }
        else
        {
            
            isVisible = true;
            if (!world.GetTile(location))
            {
                isVisible = false;

            }
            //print(movementHorizontal +"| "+ movementHorizontal);

        }
    }

    public int vision = 1;
    void UpdateFogOfWar(){
        Vector3Int currentPlayerTile = fogOfWar.WorldToCell(transform.position);

        for(int x =-vision; x<= vision; x++){
            for(int y=-vision; y<= vision; y++){
                fogOfWar.SetTile(currentPlayerTile + new Vector3Int(x, y, 0), null);
            }
        }
    }
    
}
