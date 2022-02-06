public static class StateManager
{
    public static State CurrentCursorState = State.Empty;
}

public enum State
{
    ReadyToLanding,
    Garden,
    Cucumber,
    Tomato,
    Wheat,
    CucumberComplete,
    TomatoComplete,
    WheatComplete,
    Empty
}