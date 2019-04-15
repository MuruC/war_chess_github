using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeGrid : MonoBehaviour
{
    // public GameObject hexPrefab;

    //  public int width;
    // public int height;

    // Start is called before the first frame update
    void Start()
    {
        float xOffset = GameManager.Instance.xOffset;
        float yOffset = GameManager.Instance.yOffset;
        for (int x = 0; x < GameManager.Instance.width;++x) {
            for (int y = 0; y < GameManager.Instance.height;++y) {
                float arrayXPos = x * xOffset;
                float arrayYPos = 0;
                GameObject newtile = Instantiate(GameManager.Instance.terrainPrefab);
                int[,] blackTileIndex = new int[x, y];

                GameObject newPlayer1BlackTile = Instantiate(GameManager.Instance.blackTile);
                GameObject newPlayer2BlackTile = Instantiate(GameManager.Instance.blackTile);
            
                if (y % 2 == 1) {
                    arrayXPos += xOffset/2;
                }
                
                arrayYPos = y*yOffset;
                GameManager.Instance.setTerrainGameObject(newtile,x,y);
                GameManager.Instance.setTerrainPos(x, y, arrayXPos - GameManager.Instance.xBeginPos, arrayYPos - GameManager.Instance.yBeginPos);

                newtile.name = "Terrain_" + x + "_" + y;
                newtile.GetComponent<TileScript>().x = x;
                newtile.GetComponent<TileScript>().y = y;
                GameManager.Instance.setTileIndexXDic(newtile.name, x);
                GameManager.Instance.setTileIndexYDic(newtile.name, y);
                GameManager.Instance.setTileObjectByIndex("xIndex_"+x.ToString()+"yIndex_"+y.ToString(),newtile);


                newPlayer1BlackTile.name = "player1black_" + x + "_" + y;
                newPlayer2BlackTile.name = "player2black_" + x + "_" + y;
                GameManager.Instance.setPlayer1BlackTiles(newPlayer1BlackTile.name,newPlayer1BlackTile);
                GameManager.Instance.setPlayer2BlackTiles(newPlayer2BlackTile.name, newPlayer2BlackTile);
                newPlayer1BlackTile.GetComponent<TileScript>().x = x;
                newPlayer1BlackTile.GetComponent<TileScript>().y = y;
                newPlayer2BlackTile.GetComponent<TileScript>().x = x;
                newPlayer2BlackTile.GetComponent<TileScript>().y = y;
                newPlayer1BlackTile.transform.position = newtile.transform.position;
                newPlayer2BlackTile.transform.position = newtile.transform.position;
                // GameManager.Instance.setSprite(0, x, y);
                ///*
                float spriteIndex = Random.Range(0,100);
                if (spriteIndex <= 25) {
                    GameManager.Instance.setSprite(0, x, y);
                    newtile.GetComponent<TileScript>().spriteType = 0;
                } else if (spriteIndex > 25 && spriteIndex <= 50) {
                    GameManager.Instance.setSprite(3, x, y);
                    newtile.GetComponent<TileScript>().spriteType = 3;
                } else if (spriteIndex > 50 && spriteIndex <= 65) {
                    GameManager.Instance.setSprite(1, x, y);
                    newtile.GetComponent<TileScript>().spriteType = 1;
                } else if (spriteIndex > 65 && spriteIndex < 80) {
                    GameManager.Instance.setSprite(2, x, y);
                    newtile.GetComponent<TileScript>().spriteType = 2;
                } else if (spriteIndex >= 80 && spriteIndex < 90) {
                    GameManager.Instance.setSprite(4, x, y);
                    newtile.GetComponent<TileScript>().spriteType = 4;
                } else if (spriteIndex >= 90) {
                    GameManager.Instance.setSprite(5, x, y);
                    newtile.GetComponent<TileScript>().spriteType = 5;
                }
                // */

                //生成城堡
                if (y == 0 && x == (GameManager.Instance.width-1)/2) {

                    Vector2 pos = GameManager.Instance.getTerrainPos(x,y);
                    GameManager.Instance.orangeCastle.transform.position = pos;
                }

                if (y == GameManager.Instance.height-1 && x == (GameManager.Instance.width - 1) / 2)
                {
                    Vector2 pos = GameManager.Instance.getTerrainPos(x, y);
                    GameManager.Instance.purpleCastle.transform.position = pos;
                }
            }
        }
        int middleIndexX = (GameManager.Instance.width - 1) / 2;
        int heightIndexY = GameManager.Instance.height - 1;
        GameManager.Instance.removePlayer1BlackTile(middleIndexX,0);
        GameManager.Instance.removePlayer1BlackTile(middleIndexX - 1, 0);
        GameManager.Instance.removePlayer1BlackTile(middleIndexX + 1, 0);
        GameManager.Instance.removePlayer1BlackTile(middleIndexX - 1, 1);
        GameManager.Instance.removePlayer1BlackTile(middleIndexX, 1);

        GameManager.Instance.removePlayer2BlackTile(middleIndexX, heightIndexY);
        GameManager.Instance.removePlayer2BlackTile(middleIndexX - 1, heightIndexY);
        GameManager.Instance.removePlayer2BlackTile(middleIndexX + 1, heightIndexY);
        GameManager.Instance.removePlayer2BlackTile(middleIndexX, heightIndexY-1);
        GameManager.Instance.removePlayer2BlackTile(middleIndexX + 1, heightIndexY-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
