using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridController : MonoBehaviour
{
    private float GridSize = 1f;
    private float GreedStep = 1f;
    [SerializeField]
    private GameObject CellPrefab;
    public static List<GameObject> CellList = new List<GameObject>();
    public static int NumberOfGardens;

    public void GenerateGrid(Vector2Int gridSize)
    {
        for (float i = 0.5f; i < gridSize.x; i++)
        {
            for (float j = 0.5f; j < gridSize.y; j++)
            {
                var cellGO = Instantiate(CellPrefab, new Vector3(GridSize * i, 0, GreedStep * j), Quaternion.identity);
                cellGO.transform.SetParent(gameObject.transform);
                cellGO.name = string.Format("Cell {0} {1}", i, j);
            }

        }
    }

    private void Start()
    {
        GenerateGrid(new Vector2Int(8,8));
    }

    private void Update()
    {

        ClearHightlight(gameObject.transform);
        Debug.Log(NumberOfGardens);
        if (StateManager.CurrentCursorState == State.Garden)
        {
            var dist = Mathf.Abs(transform.position.z - Camera.main.transform.position.z);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                Dictionary<GameObject, float> Distances = new Dictionary<GameObject, float>();
                var roundedPos = new Vector3(Mathf.RoundToInt(raycastHit.point.x), raycastHit.point.y, Mathf.RoundToInt(raycastHit.point.z));
                foreach (Transform cell in gameObject.transform)
                {
    
                    float distance = Vector3.Distance(roundedPos, cell.transform.position);
                    Distances.Add(cell.gameObject, distance);
                    
                    
                }
                Distances = Distances.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
                for (int i = 0; i <= 3; i++)
                {
                    if (Distances.ElementAt(i).Key.GetComponent<CellControl>().CurrentState == State.Empty)
                    {
                        Distances.ElementAt(i).Key.GetComponent<Renderer>().material.color = Color.green;
                        CellList.Add(Distances.ElementAt(i).Key);
                    }

                }
            }
        }
    }

    public void ClearHightlight(Transform cells)
    {
        if (CellList.Count > 0)
        {
            CellList.Clear();
            foreach(Transform cell in cells)
            {
                if (cell.GetComponent<CellControl>().CurrentState == State.Empty)
                    cell.GetComponent<Renderer>().material.color = Color.white;
            }
        }
        
    }
}
