using System;
using Grid;
using UnityEngine;

namespace field_of_view
{
    
    public class FieldOfView : MonoBehaviour
    {
        
        
        [SerializeField] private GridContainerSO grid;
        [SerializeField] private TileTypeContainerSO tileTypeContainer;
        [SerializeField] private bool debug = false;
        [SerializeField] private int visionRangeTest;
        [SerializeField] private Vector2Int startPosTest;

    
        public void generateVision()
        {
            GETVisibleTiles(visionRangeTest, startPosTest);
        }

        public bool[,] GETVisibleTiles(int visionRange, Vector2Int startTile)
        {
            bool[,] visibleTiles = new bool[2*visionRange+1, 2*visionRange+1];
            
            int maxwidth = grid.tileGrids[1].Width;
            int maxheight = grid.tileGrids[1].Height;

            int lowerX = Mathf.Max(0, startTile[0] - visionRange);
            int upperX = Mathf.Min(maxwidth, startTile[0] + visionRange);
            int lowerY = Mathf.Max(0, startTile[1] - visionRange);
            int upperY = Mathf.Min(maxheight, startTile[1] + visionRange);

            int offsetX = Mathf.Max(-(startTile[0] - visionRange), 0);
            int offsetY = Mathf.Max(-(startTile[1] - visionRange), 0);

            for (int i = lowerX; i < upperX; i++)
            {
                for (int j = lowerY; j < upperY; j++)
                {
                    visibleTiles[i + offsetX, j + offsetY] = CheckTile(i, j, startTile);
                }
            }

            if (debug)
            {
                string str = "\n";
                for (int i = lowerX; i < upperX; i++)
                {
                    for (int j = lowerY; j < upperY; j++)
                    {
                        int tileType = grid.tileGrids[1].GetGridObject(i, j).tileTypeID;
                        if (tileTypeContainer.tileTypes[tileType].Flags.HasFlag(ETileFlags.opaque))
                        {
                            str += "|b";
                        }
                        else if (visibleTiles[i + offsetX, j + offsetY])
                        {
                            str += "|+";
                        }
                        else
                        {
                            str += "|-";
                        }
                        
                    }

                    str += "\n";

                }
                Debug.Log(str);
            }

            return visibleTiles;
        }

        private bool CheckTile(int x1, int y1, Vector2Int startTile)
        {
            int x0 = startTile[0];
            int y0 = startTile[1];
            int tileType;
            
            int dx =  Mathf.Abs(x1-x0), sx = x0<x1 ? 1 : -1;
            int dy = -Mathf.Abs(y1-y0), sy = y0<y1 ? 1 : -1;
            int err = dx+dy, e2; /* error value e_xy */

            while (true)
            {
                //TODO: Diese Abfrage vllt int fkt auslagern und die verschiedenen ebenen einbeziehen
                
                if (x0==x1 && y0==y1) return true;
                
                tileType = grid.tileGrids[1].GetGridObject(x0, y0).tileTypeID;
                if (tileTypeContainer.tileTypes[tileType].Flags.HasFlag(ETileFlags.opaque))
                {
                    return false;
                }
                
                e2 = 2*err;
                if (e2 > dy) { err += dy; x0 += sx; } /* e_xy+e_x > 0 */
                if (e2 < dx) { err += dx; y0 += sy; } /* e_xy+e_y < 0 */
            }
        }
    }
}