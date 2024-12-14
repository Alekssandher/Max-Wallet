using Godot;


public partial class Container : Panel
{
	[Export]
	Label TitleLabel;
	[Export]
	Label WarningLabel;
	[Export]
	Label SeedLabel;

    public void FillItens(string Title, string Warning, string Seed) {
		TitleLabel.Text = Title;
		WarningLabel.Text = Warning;
		SeedLabel.Text = Seed;
		
	}
	
	public void _on_go_to_dashboard_pressed() {

		PackedScene dashboard = ResourceLoader.Load<PackedScene>("res://scenes/Dashboard.tscn");
		
		GetTree().ChangeSceneToPacked(dashboard);
	}
}
