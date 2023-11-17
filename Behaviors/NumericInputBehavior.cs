namespace Slugrace.Behaviors;

public class NumericInputBehavior : Behavior<Entry>
{
    protected override void OnAttachedTo(Entry bindable)
    {
        bindable.TextChanged += Bindable_TextChanged;
        base.OnAttachedTo(bindable);
    }

    protected override void OnDetachingFrom(Entry bindable)
    {
        bindable.TextChanged -= Bindable_TextChanged;
        base.OnDetachingFrom(bindable);
    }

    private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = (Entry)sender; 

        if (!string.IsNullOrEmpty(e.NewTextValue))
        {
            bool isNumeric = int.TryParse(e.NewTextValue, out int value);

            if (!isNumeric)
            {
                entry.Text = e.OldTextValue;
            }
        }
    }
}
