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
    public GhostPiece ghostPiece;
    [SerializeField] Vector3Int initialPos;
    [SerializeField] GameObject border;
    public Vector2Int boardSize = new Vector2Int(10, 20);
    public Bounds tileBounds; 
    private int lineLimit;
    
    private void Awake() {
        for(int i=0; i < tetrominoData.Length; i++) {
            tetrominoData[i].Initalize();
        }

        this.tilemap = GetComponentInChildren<Tilemap>();
    }

    public void Start() {
        this.piece = FindObjectOfType<Piece>();
        this.ghostPiece = FindObjectOfType<GhostPiece>();
        tileBounds = new Bounds(border.transform.position, border.transform.localScale);
        tileBounds.max += new Vector3Int(-1,0,0);
        lineLimit = (int) tileBounds.size.x + 1;

        SpawnPiece();
        Debug.Log(tileBounds.min);
        Debug.Log(tileBounds.max);


    }

    public void SpawnPiece() {
        int random = UnityEngine.Random.Range(0,tetrominoData.Length);
        TetrominoData pieceData = tetrominoData[random];

        piece.Initalize(this,initialPos,pieceData);
        ghostPiece.Initalize(piece, this);
        Set(piece);
    }

    public void Set(Piece piece) {
        for(int i = 0; i< piece.cells.Length; i++) {
            tilemap.SetTile(piece.cells[i] + piece.piecePosition ,piece.tetrominoData.tile);
        }
    }

    public void ClearBoard() {
        for(int i = 0; i< piece.cells.Length; i++) {
            tilemap.SetTile(piece.cells[i] + piece.piecePosition, null);
        }
    }
    public void LockPiece(Piece piece) {
        Set(piece);
        CheckLines();
        SpawnPiece();
    }

    public void CheckLines() {
        for(int y = (int) tileBounds.min.y; y < tileBounds.max.y ; y++) {

            int currentLine = 0;

            for(int x = (int) tileBounds.min.x; x < tileBounds.max.x + 1 ; x ++) {
                if(tilemap.GetTile(new Vector3Int(x,y,0)) != null) {
                    currentLine++;
                }
            }   
            if(currentLine == lineLimit) {
                ClearLines(y);
                Dropline(y);
                y--;
            }

        }
    }
    public void ClearLines(int yLoc) {
        for(int x = (int) tileBounds.min.x ; x < (int) tileBounds.max.x + 1; x++) {
            tilemap.SetTile(new Vector3Int(x,yLoc,0), null);
        } 
    }

    public void Dropline(int i) {
        for(int y = i; y < (int) tileBounds.max.y ; y++)  {
            for(int x = (int) tileBounds.min.x ; x < (int) tileBounds.max.x; x++) {
                TileBase upperTile = tilemap.GetTile(new Vector3Int(x,y+1,0));
                tilemap.SetTile(new Vector3Int(x,y,0), upperTile);
            }
        }
    }
}
