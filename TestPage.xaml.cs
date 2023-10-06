using Microsoft.Maui.Platform;

namespace Slugrace;

public partial class TestPage : ContentPage
{    
    public TestPage()
	{
		InitializeComponent();

        // create grid
        Grid grid = new()
        {
            Margin = 40,
            BackgroundColor = Colors.OldLace,
            RowSpacing = 10,
            ColumnSpacing = 10,
            RowDefinitions =
            {
                new RowDefinition { Height = new GridLength(100) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
            },
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) }
            }            
        };

        // add children to grid

        // Row 0 and Column 0 are the default values, so we can leave them out.
        grid.Add(new BoxView 
        { 
            Color = Colors.Red 
        });

        // We can pass the column (1) and the row (0) as arguments to the Add method.
        grid.Add(new BoxView
        {
            Color = Colors.Green
        }, 1, 0);

        // Alternatively, we can position the child with the Grid.SetRow and
        // Grid.SetColumn methods (we don't need the latter in this example
        // because the default value of 0 is used). There are also the Grid.RowSpan
        // (not used here) and Grid.ColumnSpan methods.
        BoxView blueBox = new BoxView { Color = Colors.Blue };
        Grid.SetRow(blueBox, 1);
        Grid.SetColumnSpan(blueBox, 2);
        grid.Add(blueBox);

        // set page content
        Content = grid;
    }
}
