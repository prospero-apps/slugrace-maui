namespace Slugrace
{
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

            VisualStateManager.GoToState(control, visualState);
        }
    }
}
