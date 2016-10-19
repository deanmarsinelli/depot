using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

// lay out a new level procedurally based on the level number
public class BoardManager : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    // dimensions of the game board
    public int columns = 8;
    public int rows = 8;

    // min of 5 walls per level and max of 9 walls per level
    public Count wallCount = new Count(5, 9);
    // same for food: min of 1, max of 5
    public Count foodCount = new Count(1, 5);

    // prefabs
    public GameObject exit;
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] foodTiles;
    public GameObject[] enemyTiles;
    public GameObject[] outerWallTiles;

    // parent of all tile transforms
    private Transform boardHolder;
    // used to track positions on the game board and whether an object is in that position
    private List<Vector3> gridPositions = new List<Vector3>();

    void InitializeList()
    {
        gridPositions.Clear();

        // create list of possible positions to spawn prefabs
        // outer grid tiles on the board are empty
        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < columns - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    // set up outer wall and floor
    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;

        // indices start at -1, because were creating a border around the game board
        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < columns + 1; y++)
            {
                // grab a random floor tile
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

                // if were in the border, use an outer wall tile
                if (x == -1 || x == columns || y == -1 || y == columns)
                {
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                }

                // instantiate new floor tile
                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                // set the parent transform to be boardHolder
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    Vector3 RandomPosition()
    {
        // get a random position in our grid positions list, then remove that vector3
        // so it cannot be re-used for another spawned item (no duplicates)
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);

        return randomPosition;
    }

    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        // how many objects do we spawn (max + 1, because the max is exlusive in this function)
        int objectCount = Random.Range(minimum, maximum + 1);

        for (int i = 0; i < objectCount; i++)
        {
            // choose a random position, and random tile, and instantiate it there
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }

    public void SetupScene(int level)
    {
        BoardSetup();
        InitializeList();
        LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);
        LayoutObjectAtRandom(foodTiles, foodCount.minimum, foodCount.maximum);

        // logarithmic difficulty for enemy count
        int enemyCount = (int)Mathf.Log(level, 2f);
        LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);

        // exit in upper right
        Instantiate(exit, new Vector3(columns - 1, rows - 1, 0F), Quaternion.identity);
    }
}
