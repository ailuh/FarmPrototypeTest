using UnityEngine;
using static StateManager;
using System.Collections;
using System.Drawing;
using Color = UnityEngine.Color;

public class CellControl : MonoBehaviour
{
    public State CurrentState = State.Empty;
    public Material GardenBed;
    public Material GreenGrass;
    public Material GardenBedFresh;
    public Material GardenBedComplete;

    void OnMouseDown()
    {
        if (CurrentState == State.TomatoComplete)
        {
            UIManager.Instance.TomatoCounter++;
            UpdateCounterAndClear();
        }
        else if (CurrentState == State.CucumberComplete)
        {
            UIManager.Instance.CucumberCounter++;
            UpdateCounterAndClear();
        }
        else if (CurrentState == State.WheatComplete)
        {
            UIManager.Instance.WheatCounter++;
            UpdateCounterAndClear();

        }
        else if (CurrentCursorState == State.Garden && CurrentState == State.Empty)
        {
            if (GridController.NumberOfGardens <= 36)
            {
                if (GridController.CellList.Count == 4)
                {
                    foreach (var cell in GridController.CellList)
                    {
                        cell.GetComponent<Renderer>().material = GardenBed;
                        cell.GetComponent<CellControl>().CurrentState = State.ReadyToLanding;
                    }
                    GridController.NumberOfGardens += 4;
                    GridController.CellList.Clear();
                    if (GridController.NumberOfGardens > 36)
                    {
                        CurrentCursorState = State.Empty;
                    }
                }
            }
            else
            {
                CurrentCursorState = State.Empty;
            }
        }
        else if (CurrentCursorState == State.Cucumber && CurrentState == State.ReadyToLanding)
        {
            CurrentState = State.Cucumber;
            StartCoroutine(StartGrowing(3, State.CucumberComplete, Color.green, Color.white));
        }
        else if (CurrentCursorState == State.Tomato && CurrentState == State.ReadyToLanding)
        {
            CurrentState = State.Tomato;
            StartCoroutine(StartGrowing(6, State.TomatoComplete, Color.red, Color.white));

        }
        else if (CurrentCursorState == State.Wheat && CurrentState == State.ReadyToLanding)
        {
            CurrentState = State.Wheat;
            StartCoroutine(StartGrowing(18, State.WheatComplete, Color.yellow, Color.white));
        }
    }

    private void UpdateCounterAndClear()
    {
        CurrentState = State.Empty;
        CurrentCursorState = State.Empty;
        UIManager.Instance.UpdateCounts();
        gameObject.GetComponent<Renderer>().material = GreenGrass;
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }

    IEnumerator StartGrowing(int time, State state, Color startColor, Color finishColor)
    {
        gameObject.GetComponent<Renderer>().material = GardenBedFresh;
        gameObject.GetComponent<Renderer>().material.color = startColor;
        yield return new WaitForSeconds(time);
        gameObject.GetComponent<Renderer>().material = GardenBedComplete;
        gameObject.GetComponent<Renderer>().material.color = finishColor;
        GridController.NumberOfGardens--;
        CurrentState = state;
        yield return null;
    }
}
