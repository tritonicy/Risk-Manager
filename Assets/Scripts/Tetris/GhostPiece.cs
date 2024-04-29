using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class GhostPiece : MonoBehaviour
{
    private Piece activePiece;
    private Board board;
    public Tile tile;
    private Vector3Int piecePos;
    public Vector3Int[] ghostCells{get; private set;}
    public Tilemap tilemap{get; private set;}


    public void Initalize(Piece piece, Board board) {
        this.board = board;
        this.activePiece = piece;
        this.piecePos = piece.piecePosition;

        ghostCells = new Vector3Int[4];

        for(int i = 0; i < activePiece.tetrominoData.cells.Length; i++) {
            ghostCells[i] = piece.cells[i];
        }
    }

    private void Start() {
        tilemap = GetComponentInChildren<Tilemap>();
    
    }
    private void LateUpdate() {
        Clear();
        Copy(); 
        HardDrop();
        Set();
    }
    public void Clear() {
        for(int i = 0; i < ghostCells.Length; i++) {
            tilemap.SetTile(piecePos + ghostCells[i], null);
        }
    }

    public void Copy() {
        for (int i = 0; i <ghostCells.Length; i++) {
            ghostCells[i] = activePiece.cells[i];
        }
       }

    public void HardDrop() {
        Vector3Int currentPos = activePiece.piecePosition;
        int y = (int) currentPos.y;
        int bottom = (int) board.tileBounds.min.y - 2;


        board.ClearBoard();

        for(int row = y; row > bottom ; row--) {

            currentPos.y = row;
            if(activePiece.isValidMove(currentPos,activePiece)) {
                piecePos = currentPos;
            }
            else{
                break;
            }
            
        }
        board.Set(activePiece);
    }

    public void Set() {
        for(int i = 0; i< ghostCells.Length; i++) {
            tilemap.SetTile(piecePos + ghostCells[i], tile);
        }
    }
}
