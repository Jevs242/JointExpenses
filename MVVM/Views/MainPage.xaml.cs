
namespace JointExpenses;

using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Diagnostics;

public partial class MainPage : ContentPage
{
    public BillList billList = new BillList();
    decimal total;
    Calculate calculate = new Calculate();

    public MainPage()
    {
        InitializeComponent();
    }

    public void UpdateUI()
    {
        var entryTax = this.FindByName<Entry>("entryTax");
        var entryTaxRate = this.FindByName<Entry>("entryTaxRate");
        var entryPerson = this.FindByName<Entry>("entryPerson");
        var entryTip = this.FindByName<Entry>("entryTip");


        entryTax.SetBinding(Entry.TextProperty, new Binding(nameof(calculate.TotalTax), source: (object)calculate));
        entryTaxRate.SetBinding(Entry.TextProperty, new Binding(nameof(calculate.PercentTax), source: (object)calculate));
        entryPerson.SetBinding(Entry.TextProperty, new Binding(nameof(calculate.TotalPerson), source: (object)calculate));
        entryTip.SetBinding(Entry.TextProperty, new Binding(nameof(calculate.Tip), source: (object)calculate));

        var bothScroll = this.FindByName<StackLayout>("bothScroll");
        var ownScroll = this.FindByName<StackLayout>("ownScroll");

        bothScroll.Children.Clear();
        ownScroll.Children.Clear();

        for (int i = 0; i < billList.Bill.Count; i++)
        {
            Bill bill = billList.Bill[i];
            var num = new Label { Text = $"{i + 1}", TextColor = Color.FromRgb(224, 225, 221), HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };
            var label = new Label { Text = $"${bill.Cost.ToString("0,0.00")}" ,TextColor = Color.FromRgb(224, 225, 221), HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };
            var button = new Button { Text = "x", HorizontalOptions = LayoutOptions.End, BackgroundColor = Color.FromRgb(224, 225, 221) };
            button.Clicked += RemoveButton_Clicked;
            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Star) });

            grid.Add(num, 0, 0);
            grid.Add(label, 1, 0);
            grid.Add(button, 2, 0);


            if (bill.IsBoth)
            {
                bothScroll.Children.Add(new Frame { Content = grid, BackgroundColor = Color.FromRgb(50, 50, 50), Padding = 1, HorizontalOptions = LayoutOptions.Fill, BorderColor = Color.FromRgb(0, 0, 0) });
            }
            else
            {
                ownScroll.Children.Add(new Frame { Content = grid, BackgroundColor = Color.FromRgb(50, 50, 50), Padding = 1, BorderColor = Color.FromRgb(0, 0, 0) });
            }
        }
    }

    

    private void Button_Clicked(object sender, EventArgs e)
    {
        total = 0;

        for (int i = 0; i < billList.Bill.Count; i++)
        {
            Bill bill = billList.Bill[i];
            total += bill.IsBoth ? bill.Cost / calculate.TotalPerson : bill.Cost; 

        }

        if(this.FindByName<RadioButton>("taxTotalRadio").IsChecked)
        {
            decimal totalTax = calculate.TotalTax / calculate.TotalPerson;
            total += totalTax;
            var tipLabel = this.FindByName<Label>("taxTotal");
            tipLabel.Text = "Tax : $" + totalTax.ToString("0,0.00");
        }
        else
        {
            decimal totalTax = (total * (calculate.PercentTax / 100));

            total += totalTax;

            var taxLabel = this.FindByName<Label>("taxTotal");
            taxLabel.Text = "Tax : $" + totalTax.ToString("0,0.00");
        }

        if (total != 0)
        {
            decimal tipTotal = calculate.Tip / (decimal)calculate.TotalPerson;
            total += tipTotal;
            var tipLabel = this.FindByName<Label>("tipTotal");
            tipLabel.Text = "Tip : $" + (calculate.Tip / calculate.TotalPerson).ToString("0,0.00");
        }

        var totalLabel = this.FindByName<Label>("totalLabel");
        totalLabel.Text = "$" + total.ToString("0,0.00");
    }

    private void AddButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddBill());
    }

    private void RemoveButton_Clicked(Object sender, EventArgs e)
    {
        Button button = (Button)sender;

        Element parentElement = button.Parent;

        Grid grid = (Grid)parentElement;
        var row = Grid.GetRow(button);
        var col = Grid.GetColumn(button);
        var otherChild = grid.Children.Cast<View>().FirstOrDefault(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == col  - 2);

        if (otherChild != null)
        {
            // Do something with otherChild
            Debug.WriteLine(otherChild);

            Label idLabel = (Label)otherChild;

            billList.Bill.Remove(billList.Bill[int.Parse(idLabel.Text) - 1]);
        }
        UpdateUI();



    }

    private void Entry_PropertyChanged(object sender, TextChangedEventArgs e)
    {
        var entry = (Entry)sender;
        var newText = e.NewTextValue;

        // Remove any unwanted characters from the input text
        newText = FilterInput(newText);

        int number;
        if (this.FindByName<Entry>("entryPerson") == entry && int.TryParse(newText , out number))
        {
            number = int.Parse(newText);
            number = Math.Clamp(number, 2, 9999999);
            newText = number.ToString();
            var personLabel = this.FindByName<Label>("personAmount");
            personLabel.Text = "People : " + newText.ToString();
        }



        // Update the text of the entry control
        entry.Text = newText;
    }

    private string FilterInput(string input)
    {
        // Implement your filtering logic here
        // For example, to allow only numeric input:
        return new string(input.Where(c => char.IsDigit(c) || c == '.' ).ToArray() );
    }
}
