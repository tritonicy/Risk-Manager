using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        tileBounds = new Bounds(
            border.transform.position,
            border.transform.localScale
        );
        tileBounds.max += Vector3.left;
        this.piece = FindObjectOfType<Piece>();
        SpawnPiece();

    }

    public void SpawnPiece() {
        int random = Random.Range(0,tetrominoData.Length);
        TetrominoData pieceData = tetrominoData[random];

        piece.Initalize(this,initialPos,pieceData);
        Set(piece);
    }

    public void Set(Piece piece) {
        for(int i = 0; i< piece.cells.Length; i++) {
            tilemap.SetTile(piece.cells[i] ,piece.tetrominoData.tile);
        }
    }

    public void ClearBoard() {
//        for(int i = tilemap.cellBounds.min.x; i < tilemap.cellBounds.max.x; i++) {
//            for(int j = tilemap.cellBounds.min.y; j < tilemap.cellBounds.max.y; j++) {
//                tilemap.SetTile(new Vector3Int(i,j,0), null);
//            }
//        }
        for(int i = 0; i< piece.cells.Length; i++) {
            tilemap.SetTile(piece.cells[i], null);
        }
    }
}
