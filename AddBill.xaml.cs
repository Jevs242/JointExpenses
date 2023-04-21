namespace Split;

public partial class AddBill : ContentPage
{
	public AddBill()
	{
		InitializeComponent();
	}

    private void ButtonAdd_Pressed(object sender, EventArgs e)
    {
        var ownRadio = this.FindByName<RadioButton>("ownRadio");
        var bothRadio = this.FindByName<RadioButton>("bothRadio");
        var entry = this.FindByName<Entry>("priceEntry");

        if(ownRadio.IsChecked || bothRadio.IsChecked)
        {
            if (Navigation.NavigationStack.Count > 1)
            {
                var previousPage = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2] as MainPage;
                Bill bill = new Bill(decimal.Parse(entry.Text) , bothRadio.IsChecked);
                previousPage.billList.Bill.Add(bill);
                previousPage.UpdateUI();
            }
            Navigation.PopAsync();
        }

    }

    private void ButtonCancel_Pressed(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private void Entry_PropertyChanged(object sender, TextChangedEventArgs e)
    {
        var entry = (Entry)sender;
        string newText = e.NewTextValue;

        // Remove any unwanted characters from the input text
        newText = FilterInput(newText);

        // Update the text of the entry control
        entry.Text = newText;
    }

    private string FilterInput(string input)
    {
        // Implement your filtering logic here
        // For example, to allow only numeric input:
        return new string(input.Where(c => char.IsDigit(c) || c == '.').ToArray());
    }
}