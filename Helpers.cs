namespace Slugrace;

public static class Helpers
{
    public static void ValidateNumericInputAndSetState(string enteredText, int min, int max, VisualElement control)
    {
        bool isNumeric = int.TryParse(enteredText, out int numericValue);
        bool isInRange = isNumeric && numericValue >= min && numericValue <= max;
        bool isValid = isInRange;
        bool isEmpty = enteredText == string.Empty;

        string visualState = isValid ? "Valid" : "Invalid";

        if (isEmpty)
        {
            visualState = "Empty";
        }

        if (control != null)
        {
            VisualStateManager.GoToState(control, visualState);
        }
    }
        
    public static void ValidateNumericInputAndSetState(string enteredText, bool isInRange, VisualElement control)
    {
        bool isNumeric = int.TryParse(enteredText, out int _);
        bool isValid = isNumeric && isInRange;
        bool isEmpty = enteredText == string.Empty;

        string visualState = isValid ? "Valid" : "Invalid";

        if (isEmpty)
        {
            visualState = "Empty";
        }

        VisualStateManager.GoToState(control, visualState);
    }
   
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
