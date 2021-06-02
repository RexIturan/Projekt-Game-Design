using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace {

    public struct node {
        public int id;
        public int cost;
        public List<int> neighbours;
        public int parent;
    }
    
    public class Pathfinding {
        // A* Pathfinding for getting all possible moves from a 2d array

        private node[] graph;
        private int width;
        private int height;

        public int coordToIndex(int x, int y) {
            return y + x * height;
        }

        public Vector2Int indexToCoord(int index) {
            return new Vector2Int(index / height, index % height);
        }
        
        public node[] initPathfindingMap(int[,] map) {
            // create graph? or get neighbours graph and 2d array

            width = map.GetLength(0);
            height = map.GetLength(1);

            // Debug.Log(width + " " + height);
            
            graph = new node[width * height];
            // Debug.Log(graph.Length);
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    node tempNode = new node() {
                        id = coordToIndex(x, y),
                        cost = map[x, y],
                    };
                    
                    // get neighbours N E S W
                    List<int> neighbours = new List<int>();
                    // S
                    if (y -1 >= 0 && map[x, y - 1] != null) {
                        neighbours.Add(coordToIndex(x, y -1));
                    }
                    // N
                    if (y +1 < height && map[x, y + 1] != null) {
                        neighbours.Add(coordToIndex(x, y + 1));
                    }
                    // W
                    if (x -1 >= 0 && map[x - 1, y] != null) {
                        neighbours.Add(coordToIndex(x - 1, y));
                    }
                    // E
                    if (x +1 < width && map[x + 1, y] != null) {
                        neighbours.Add(coordToIndex(x + 1, y));
                    }

                    tempNode.neighbours = neighbours;
                    graph[coordToIndex(x, y)] = tempNode;
                }
            }

            return graph;
        }

        public HashSet<node> getPossiblePaths(int x, int y, int speed) {
            // set start position
            // returns list of reachable tiles as a graph && reference array?
            HashSet<node> working = new HashSet<node>();
            HashSet<node> temp = new HashSet<node>();
            HashSet<node> final = new HashSet<node>();

            int dist = speed;

            if (x < 0 || x >= width || y < 0 || y >= height) {
                return new HashSet<node>();
            }
            
            final.Add(graph[coordToIndex(x, y)]);
            

            foreach (var node in final) {
                foreach (var id in node.neighbours) {
                    if (dist - graph[id].cost >= 0) {
                        node n = graph[id];
                        n.parent = node.id;
                        working.Add(n);    
                    }
                }
            }

            final.UnionWith(working);
            
            while (working.Count > 0) {
                foreach (var node in working) {
                    foreach (var id in node.neighbours) {
                        if (dist - (graph[id].cost + node.cost) >= 0) {
                            node n = graph[id];
                            n.cost += node.cost;
                            n.parent = node.id;
                            
                            bool better = true;
                            foreach (var finalNode in final) {
                                if (finalNode.id == id) {
                                    if (finalNode.cost <= n.cost) {
                                        better = false;    
                                    }
                                    else {
                                        final.Remove(finalNode);    
                                    }
                                }
                            }
                            if (better) {
                                if (temp.Count > 0) {
                                    var removeSet = new HashSet<node>();
                                    var addSet = new HashSet<node>();
                                    bool add = true;
                                    foreach (var tempNode in temp) {
                                        if (tempNode.id == id) {
                                            add = false;
                                            if (tempNode.cost > n.cost) {
                                                removeSet.Add(tempNode);
                                                add = true;
                                            }
                                        }
                                    }
                                    if (add) {
                                        temp.Add(n);    
                                    }
                                    temp.ExceptWith(removeSet);
                                }
                                else {
                                    temp.Add(n);   
                                }
                            }
                        }
                    }
                }

                working = temp;
                final.UnionWith(working);
                temp = new HashSet<node>();
            }

            return final;
        }

        
    }
}