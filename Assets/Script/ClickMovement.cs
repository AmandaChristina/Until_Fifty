﻿using System.Collections;
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
    int lances = 50;
    int lancesMax = 50;
    [SerializeField]
    int custoTotal;

    public string nomeTileMap;

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
    }
    void Raycast2DMethod()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit){

            nomeTileMap = hit.transform.name;
            //print(tileMouseOver);
        }
    }
    void MovementClickMethod()
    {
        if (world.GetTile(location)){
            //print(worldCelltoWorld);
            novaPostionPlayer = Vector3.Lerp(transform.position, worldCelltoWorld, 24);
            transform.position = novaPostionPlayer;
            UpdateFogOfWar();
        }
        else //print("não tem");

        custosMethod(1);
        //print("Mover +1");
        lances -= custoTotal;
        custoTotal = 0;
        //colocando um limitador
    }
    void custosMethod(int custoLance)
    {
        custoTotal += custoLance;
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
            //print("vai não");
        }
        else{
            //print(movementHorizontal +"| "+ movementHorizontal);
            MovementClickMethod();
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
    void OnTriggerEnter2D(Collider2D objeto)
   {
    //    if (objeto.gameObject.layer > gameObject.layer){

    //        print("Player: "+ gameObject.layer+", Objeto:" + objeto.gameObject.layer );
    //        custosMethod(1);
    //        gameObject.layer = objeto.gameObject.layer;
    //        print("Subir Elevação +1" + objeto.gameObject.name);
    //    }

    //    else if (objeto.gameObject.layer <= gameObject.layer){
    //        gameObject.layer = objeto.gameObject.layer;
    //    }

    //    else if (objeto.gameObject.tag == "Pedra"){
    //        print("Pedra +1");
    //        custosMethod(1);
    //    }
    }
}
