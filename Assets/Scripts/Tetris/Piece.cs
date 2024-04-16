using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Piece : MonoBehaviour
{
    public Board board {get; private set;}
    public Vector3Int piecePosition{get; private set;}
    public Vector3Int[] cells{get; private set;}
    public TetrominoData tetrominoData{get; private set;}


    public void Initalize(Board board, Vector3Int piecePosition, TetrominoData tetrominoData) {
        this.board = board;
        this.piecePosition = piecePosition;
        this.tetrominoData = tetrominoData;

        cells = new Vector3Int[4];

        for(int i= 0; i< tetrominoData.cells.Length; i++) {
            cells[i] = (Vector3Int) tetrominoData.cells[i];
        }
    }

    public void Move(Vector3Int dir, Board board) {

            for(int i = 0 ; i < cells.Length; i++) {
                cells[i] += dir;
            }
        
    }

    public bool isValidMove(Vector3Int dir, Board board) {
        Vector3Int[] checkCells = cells;

        for(int i = 0; i< checkCells.Length; i++) {
            checkCells[i] += dir;

            if(!board.tileBounds.Contains(checkCells[i])) {
                return false;
            }
        }
        return true;
    }
        
}

