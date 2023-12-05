namespace Slugrace;

public static class Helpers
{   
    public static bool ValueIsInRange(int value, int min, int max) => value >= min && value <= max;

    public static void HandleNumericEntryState(bool testedValueIsValid, Entry entry)
    {
        string visualState = testedValueIsValid ? "Valid" : "Invalid";

        if (entry != null)
        {
            bool isEmpty = entry.Text == string.Empty;

            if (isEmpty)
            {
                visualState = "Empty";
            }

            VisualStateManager.GoToState(entry, visualState);
        }
    }
}
