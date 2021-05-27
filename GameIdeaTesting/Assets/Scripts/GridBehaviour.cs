using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBehaviour : MonoBehaviour
{

    // Versuch den Algorihtmus Wavefront nachzubauen, um ein Charakter auf einem Grid hin und her zu bewegen.
    public bool findDistance = false;

    public int rows = 10;

    public int cols = 10;

    public int scale = 1;

    public GameObject gridPrefab;

    public Vector3 leftBottomLocation = new Vector3(0, 0, 0);

    public GameObject[,] GridArr;

    public int startX = 0;

    public int startY = 0;

    public int endX = 0;
    public int endY = 0;

    public List<GameObject> path = new List<GameObject>();
    
    // Start is called before the first frame update
    void Awake()
    {
        GridArr = new GameObject[cols, rows];
        if (gridPrefab)
        {
            GenerateGrid();
        }
        else
        {
            print("gridPrefab fehlt, bitte füge Asset hinzu.");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (findDistance)
        {
            SetDistance();
            SetPath();
            findDistance = false;
        }
        
    }

    void GenerateGrid()
    {
        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                GameObject obj = Instantiate(gridPrefab, new Vector3(leftBottomLocation.x+scale*x,leftBottomLocation.y,leftBottomLocation.z+scale*y ), Quaternion.identity);
                obj.transform.SetParent(gameObject.transform);
                obj.GetComponent<GridStat>().x = x;
                obj.GetComponent<GridStat>().y = y;
                GridArr[x, y] = obj;

            }
        }
        
    }

    void InitialSetUp()
    {
        // Jedem Tile ersteinmal den Wert -1 zuweisen
        foreach (GameObject obj in GridArr)
        {
            obj.GetComponent<GridStat>().visited = -1;
        }

        // Bis auf den Teil 
        GridArr[startX, startY].GetComponent<GridStat>().visited = 0;
    }

    void SetPath()
    {
        int step;
        int x = endX;
        int y = endY;
        List<GameObject> tempList = new List<GameObject>();
        path.Clear();
        if (GridArr[endX,endY] && GridArr[endX,endY].GetComponent<GridStat>().visited>0)
        {
            // Koordinate zum Weg hinzufügen
            path.Add(GridArr[x,y]);
            // Step kleiner machen und nach den nächsten Suchen
            step = GridArr[x, y].GetComponent<GridStat>().visited - 1;
        }
        else
        {
            // Es konnte kein Weg gefunden werden, die Methode bricht ab
            print("Es existiert kein Weg zum Ziel");
            return;
        }

        // Immer nach den nächsten Steps suchen
        for (int i = step; i > -1; i--)
        {
            if (TestDirection(x,y,i,1))
            {
                tempList.Add(GridArr[x,y+1]);
            }
            if (TestDirection(x,y,i,2))
            {
                tempList.Add(GridArr[x+1,y]);
            }
            if (TestDirection(x,y,i,3))
            {
                tempList.Add(GridArr[x,y-1]);
            }
            if (TestDirection(x,y,i,4))
            {
                tempList.Add(GridArr[x-1,y]);
            }
            
            // Das Objekt, das am nächsten dran ist, wird zum Path hinzugefügt.
            GameObject tempObj = FindClosest(GridArr[endX, endY].transform, tempList);
            path.Add(tempObj);

            x = tempObj.GetComponent<GridStat>().x;
            y = tempObj.GetComponent<GridStat>().y;
            tempList.Clear();
        }
    }

    bool TestDirection(int x, int y, int step, int direction)
    {
        // direction sagt auch welche Richtung getestet werden soll
        // 1 ist oben, 2 ist rechts, 3 ist unten und 4 ist links
        bool ret = false;

        switch (direction)
        {
            case 1:
                if (y + 1 < rows && GridArr[x, y + 1] && GridArr[x, y + 1].GetComponent<GridStat>().visited == step)
                {
                    ret = true;
                }
                break;
            case 2:
                if (x+1 < cols && GridArr[x+1, y] && GridArr[x+1, y].GetComponent<GridStat>().visited == step)
                {
                    ret = true;
                }
                break;
            case 3:
                if (y - 1 > -1 && GridArr[x, y - 1] && GridArr[x, y - 1].GetComponent<GridStat>().visited == step)
                {
                    ret = true;
                }
                break;
            case 4:
                if (x-1 > -1 && GridArr[x-1, y] && GridArr[x-1, y].GetComponent<GridStat>().visited == step)
                {
                    ret = true;
                }
                break;
        }

        return ret;
    }

    void SetVisited(int x, int y, int step)
    {
        // Wenn das Objekt existiert dann..
        if (GridArr[x,y])
        {
            GridArr[x, y].GetComponent<GridStat>().visited = step;

        }
        
    }

    void SetDistance()
    {
        InitialSetUp();
        int x = startX;
        int y = startY;
        int size = rows * cols;
        // Ist erstmal nur die größtmögliche Bewegungsfreiheit
        int[] testArray = new int[size];

        for (int step = 1; step < size; step++)
        {
            foreach (GameObject obj in GridArr)
            {
                if (obj && obj.GetComponent<GridStat>().visited == step-1)
                {
                    TestAllDirections(obj.GetComponent<GridStat>().x, obj.GetComponent<GridStat>().y, step);
                }
            }
            
        }
    }

    void TestAllDirections(int x , int y, int step)
    {
        // Testen aller Richtungen, noch per Hand sollte aber EZ mit ner Schleife möglich sein
        // UP
        if (TestDirection(x,y,-1,1))
        {
            SetVisited(x,y+1,step);
        }
        // RIGHT
        if (TestDirection(x,y,-1,2))
        {
            SetVisited(x+1,y,step);
        }
        // BOTTOM
        if (TestDirection(x,y,-1,3))
        {
            SetVisited(x,y-1,step);
        }
        // LEFT
        if (TestDirection(x,y,-1,4))
        {
            SetVisited(x-1,y,step);
        }
    }

    GameObject FindClosest(Transform targetLocation, List<GameObject> list)
    {
        float currentDistance = scale * rows * cols;
        int index = 0;

        for (int i = 0; i < list.Count; i++)
        {
            if (Vector3.Distance(targetLocation.position, list[i].transform.position) < currentDistance)
            {
                currentDistance = Vector3.Distance(targetLocation.position, list[i].transform.position);
                index = i;
            }
        }

        return list[index];

    }
}
