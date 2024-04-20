using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class Piece : MonoBehaviour
{
    public Board board {get; private set;}
    public Vector3Int piecePosition{get; private set;}
    public Vector3Int[] cells{get; private set;}
    public TetrominoData tetrominoData{get; private set;}
    public int rotationIndex = 0;

    private void Start() {
        board = FindObjectOfType<Board>();
    }
    public void Update() {
        board.ClearBoard();

        if(Input.GetKeyDown(KeyCode.Q)) {
            Rotate(-1);
        }
        if(Input.GetKeyDown(KeyCode.E)) {
            Rotate(1);
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow)){

            Move(Vector3Int.left, board);
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)){

            Move(Vector3Int.right, board);
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)){

            Move(Vector3Int.down, board);
        }
        if(Input.GetKeyDown(KeyCode.Space)) {

            MoveAllWay(Vector3Int.down, board);
        }

        board.Set(this);
    }
    public void Initalize(Board board, Vector3Int piecePosition, TetrominoData tetrominoData) {
        this.board = board;
        this.tetrominoData = tetrominoData;
        this.piecePosition = piecePosition;

        cells = new Vector3Int[4];

        for(int i= 0; i< tetrominoData.cells.Length; i++) {
            cells[i] = (Vector3Int) tetrominoData.cells[i] + piecePosition;
        }
    }

    public void Move(Vector3Int dir, Board board) {

        if(isValidMove(dir,board)) { 
            for(int i = 0 ; i < cells.Length; i++) {
                cells[i] += dir;
                Debug.Log(cells[i]);
            }
        }
    }

    public bool isValidMove(Vector3Int dir, Board board) {
        Vector3Int[] checkCells = new Vector3Int[4];

        for(int i = 0; i< checkCells.Length; i++) {
            checkCells[i] = cells[i] + dir;

            if(!board.tileBounds.Contains(checkCells[i])) {
                return false;
            }
            if(board.tilemap.GetTile(checkCells[i]) != null) {
                return false;
            }
        }
        return true;
    }
    
    public void MoveAllWay(Vector3Int dir, Board board) {
        while(isValidMove(dir, board)) {
            Move(dir, board);
        }
    }

    public void Rotate(int direction) {
        int newDir = SetRotateDirection(rotationIndex,direction);

        int x,y;
        
        for(int i = 0; i < cells.Length; i++) {
            Vector3 cell = cells[i];

            switch(this.tetrominoData.tetromino) {
                case Tetromino.I:
                case Tetromino.O:

                cell.x -= 0.5f;
                cell.y -= 0.5f;

                x = Mathf.CeilToInt(cell.x * this.tetrominoData.rotation[0] + cell.y * this.tetrominoData.rotation[1]);
                y = Mathf.CeilToInt(cell.x * this.tetrominoData.rotation[2] + cell.y * this.tetrominoData.rotation[3]);
                break;

                default:
                x = Mathf.RoundToInt(cell.x * this.tetrominoData.rotation[0] + cell.y * this.tetrominoData.rotation[1]);
                y = Mathf.RoundToInt(cell.x * this.tetrominoData.rotation[2] + cell.y * this.tetrominoData.rotation[3]);
                break;
                
            }

            cells[i] = new Vector3Int(x, y, 0);
        }
    }

    public int SetRotateDirection(int currentDir, int direction) {
        if(currentDir + direction < 0) {
            return 4;
        }
        else if(currentDir + direction > 3) {
            return 0;
        }
        else { 
            return currentDir + direction;
        }
    }
}

