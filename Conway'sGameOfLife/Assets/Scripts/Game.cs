using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private static int SCREEN_WIDTH = 64;
    private static int SCREEN_HEIGHT = 48;
    Cell[,] grid = new Cell[SCREEN_WIDTH, SCREEN_HEIGHT]; // multidemenstional array that contains x,y values for cells

    public float fSpeed = 0.1f;

    private float fTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        PlaceCells();
    }

    // Update is called once per frame
    void Update()
    {
        if (fTimer >= fSpeed) {
            fTimer = 0;
            CountNeighbours();
            PopulationControl();
		} else {
            fTimer += Time.deltaTime;
        }
    }

    public void PlaceCells() {
        for(int y = 0; y < SCREEN_HEIGHT; y++) {
            for(int x = 0; x < SCREEN_WIDTH; x++) {
                Cell cell = Instantiate(Resources.Load("Prefabs/Cell", typeof(Cell)), new Vector2(x, y), Quaternion.identity) as Cell;
                grid[x,y] = cell;
                grid[x, y].SetAlive(RandomAliveCell());
			}
		}
	}

    void CountNeighbours() {
        for(int y = 0; y < SCREEN_HEIGHT; y++) {
            for(int x = 0; x < SCREEN_WIDTH; x++) {
                int numNeighbours = 0;

                // North
                // 0 1 0
                // 0 x 0 
                // 0 0 0
                if(y + 1 < SCREEN_HEIGHT) {
                    if(grid[x, y + 1].bIsAlive) {
                        numNeighbours++;
					}
				}

                // East
                // 0 0 0
                // 0 x 1
                // 0 0 0
                if (x + 1 < SCREEN_WIDTH) {
                    if(grid[x + 1, y].bIsAlive) {
                        numNeighbours++;
					}
				}

                // South
                // 0 0 0
                // 0 x 0 
                // 0 1 0
                if (y - 1 >= 0) {
                    if(grid[x, y - 1].bIsAlive) {
                        numNeighbours++;
					}
				}

                // West
                // 0 0 0
                // 1 x 0 
                // 0 0 0
                if (x - 1 >= 0) {
                    if (grid[x - 1, y].bIsAlive) {
                        numNeighbours++;
                    }
                }

                // NorthEast
                // 0 0 1
                // 0 x 0 
                // 0 0 0
                if (x + 1 < SCREEN_WIDTH && y + 1 < SCREEN_HEIGHT) {
                    if(grid[x + 1, y + 1].bIsAlive) {
                        numNeighbours++;
					}
				}

                // NorthWest
                // 1 0 0
                // 0 x 0 
                // 0 0 0
                if (x - 1 >= 0 && y + 1 < SCREEN_HEIGHT) {
                    if (grid[x - 1, y + 1].bIsAlive) {
                        numNeighbours++;
                    }
                }

                // SouthEast
                // 0 0 0
                // 0 x 0 
                // 0 0 1
                if (x + 1 < SCREEN_WIDTH && y - 1 >= 0) {
                    if(grid[x + 1, y - 1].bIsAlive) {
                        numNeighbours++;
					}
				}

                // SouthWest
                // 0 0 0
                // 0 x 0 
                // 1 0 0
                if (x - 1 >= 0 && y - 1 >= 0) {
                    if(grid[x - 1, y - 1].bIsAlive) {
                        numNeighbours++;
					}
				}

                grid[x, y].iNumOfNeighbours = numNeighbours;
            }
		}
	}

    void PopulationControl() {
        for (int y = 0; y < SCREEN_HEIGHT; y++) {
            for (int x = 0; x < SCREEN_WIDTH; x++) {
				if (grid[x, y].bIsAlive) {
                    if(grid[x,y].iNumOfNeighbours != 2 && grid[x,y]. iNumOfNeighbours != 3) {
                        grid[x, y].SetAlive(false);
					}
				} else {
                    if (grid[x, y].iNumOfNeighbours == 3) {
                        grid[x, y].SetAlive(true);
					}
				}
            }
        }
    }

    bool RandomAliveCell() {
        int rand = UnityEngine.Random.Range(0, 100);

        if(rand > 75) {
            return true;
		}

        return false;
	}
}
