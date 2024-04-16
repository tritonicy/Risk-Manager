using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    [SerializeField] TetrominoData[] tetrominoData;
    public Tilemap tilemap {get;private set;}
    public Piece piece;
    [SerializeField] Vector3Int initialPos;
    [SerializeField] GameObject border;
    public Bounds tileBounds;   
    private void Awake() {
        for(int i=0; i < tetrominoData.Length; i++) {
            tetrominoData[i].Initalize();
        }

        this.tilemap = GetComponentInChildren<Tilemap>();
    }

    public void Start() {
        this.piece = FindObjectOfType<Piece>();
        SpawnPiece();

        tileBounds = new Bounds(
            border.transform.position,
            border.transform.localScale
        );
    }

    public void Update() {  
        
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            ClearBoard();
            piece.Move(Vector3Int.left, this);
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            ClearBoard();
            piece.Move(Vector3Int.right, this);
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            ClearBoard();
            piece.Move(Vector3Int.down, this);
        }

        Set(piece);
    }

    public void SpawnPiece() {
        int random = Random.Range(0,tetrominoData.Length);
        TetrominoData pieceData = tetrominoData[random];

        piece.Initalize(this,initialPos,pieceData);
        Set(piece);
    }

    public void Set(Piece piece) {
        for(int i = 0; i< piece.cells.Length; i++) {
            tilemap.SetTile(piece.cells[i] + piece.piecePosition,piece.tetrominoData.tile);
        }
    }

    public void ClearBoard() {
        for(int i = tilemap.cellBounds.min.x; i < tilemap.cellBounds.max.x; i++) {
            for(int j = tilemap.cellBounds.min.y; j < tilemap.cellBounds.max.y; j++) {
                tilemap.SetTile(new Vector3Int(i,j,0), null);
            }
        }
    }
}
