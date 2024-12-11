using Godot;


public partial class GoBack : Node2D
{
	[Export]
	Button GoBackButton;

	private void _on_button_pressed () {

		PackedScene posLog = ResourceLoader.Load<PackedScene>("res://scenes/PosLog.tscn");

		GetTree().ChangeSceneToPacked(posLog);
	}
}
