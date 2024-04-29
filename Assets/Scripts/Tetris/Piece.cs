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
    private float elapsedFallTime = 0f;
    private float elapsedLockTime = 0f;
    [SerializeField] float initialLockTime = 2f;
    [SerializeField] float fallTime = 1f;
    private float lockTime;

    private void Start() {
        board = FindObjectOfType<Board>();
    }
    public void Update() {
        board.ClearBoard();

        RunMoveInputs();

        elapsedFallTime += Time.deltaTime;
        elapsedLockTime += Time.deltaTime;

        if(fallTime < elapsedFallTime) {
            if(isValidMove(Vector3Int.down, board)) {
                Move(Vector3Int.down, board);
                elapsedFallTime = 0f;

            }
            else{
                lockTime -= Time.deltaTime/1.33f;
            }
        }
        

        if(lockTime < elapsedLockTime) {
            board.LockPiece(this);
            elapsedLockTime = 0f;
        }

        board.Set(this);
        

    }

    public void RunMoveInputs() {

        if(Input.GetKeyDown(KeyCode.Q)) {
            Rotate(-1);
        }
        else if(Input.GetKeyDown(KeyCode.E)) {
            Rotate(1);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow)){
            Move(Vector3Int.left, board);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow)){

            Move(Vector3Int.right, board);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow)){

            Move(Vector3Int.down, board);
        }
        else if(Input.GetKeyDown(KeyCode.Space)) {
            
            MoveAllWay(board);
        }

        
    }
    public void Initalize(Board board, Vector3Int piecePosition, TetrominoData tetrominoData) {
        this.board = board;
        this.tetrominoData = tetrominoData;
        this.piecePosition = piecePosition;
        this.lockTime = initialLockTime;

        cells = new Vector3Int[4];

        for(int i= 0; i< tetrominoData.cells.Length; i++) {
            cells[i] = (Vector3Int) tetrominoData.cells[i];
        }
    }

    public void Move(Vector3Int dir, Board board) {

        Vector3Int newPosition = this.piecePosition;
        newPosition.x += dir.x;
        newPosition.y += dir.y;

        if(isValidMove(dir,board)) {
            elapsedLockTime = 0f;
            piecePosition = newPosition;
        }
    }

    public bool isValidMove(Vector3Int dir, Board board) {

        for(int i = 0; i< cells.Length; i++) {
            Vector3Int checkCell = cells[i] +  dir + piecePosition;

            if(!board.tileBounds.Contains(checkCell)) {
                return false;
            }

            if(board.tilemap.GetTile(checkCell) != null) {
                return false;
            }
        }
        return true;
    }
    
    public void MoveAllWay(Board board) {
        while(isValidMove(Vector3Int.down, board)) {
            Move(Vector3Int.down, board);
        }
        board.LockPiece(this);
    }

    public void Rotate(int direction) {
        int oldDir = rotationIndex;
        rotationIndex = Wrap(rotationIndex + direction, 0 ,4);

        AppylRotation(direction);

        if(!TestWallKick(oldDir,direction)) {
            rotationIndex = oldDir;
            AppylRotation(-direction);
        }
    }

    public void AppylRotation(int direction) {
        int x,y;
        
        for(int i = 0; i < cells.Length; i++) {
            Vector3 cell = cells[i];

            switch(this.tetrominoData.tetromino) {
                case Tetromino.I:
                case Tetromino.O:

                cell.x -= 0.5f;
                cell.y -= 0.5f;

                x = Mathf.CeilToInt((cell.x * this.tetrominoData.rotation[0] * direction) + (cell.y * this.tetrominoData.rotation[1] * direction));
                y = Mathf.CeilToInt((cell.x * this.tetrominoData.rotation[2] * direction) + (cell.y * this.tetrominoData.rotation[3] * direction));
                break;

                default:
                x = Mathf.RoundToInt((cell.x * this.tetrominoData.rotation[0] * direction )+ (cell.y * this.tetrominoData.rotation[1] * direction));
                y = Mathf.RoundToInt((cell.x * this.tetrominoData.rotation[2] * direction )+ (cell.y * this.tetrominoData.rotation[3] * direction));
                break;
                
            }

            cells[i] = new Vector3Int(x, y, 0);
        }
    }

    private int Wrap(int input, int min, int max)
    {
        if (input < min) {
            return max - (min - input) % (max - min);
        } else {
            return min + (input - min) % (max - min);
        }
    }

    public int ApplyWallKick(int rotationIndex, int rotateDir) {
        int dirIndex = rotationIndex * 2;

        if(rotateDir < 0) {
            dirIndex--;
        }
        return Wrap(dirIndex, 0, 8);
    }

    public bool TestWallKick(int rotationIndex, int rotateDir) {
        int dirIndex = ApplyWallKick(rotationIndex, rotateDir);
        
        for(int i = 0; i < tetrominoData.wallKicks.GetLength(1); i++) {
            if(isValidMove((Vector3Int) tetrominoData.wallKicks[dirIndex,i], board) ) {
                Move((Vector3Int) tetrominoData.wallKicks[dirIndex,i], board);
                return true;
            }
        }
        return false;
    }

}