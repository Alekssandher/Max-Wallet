using Godot;

public partial class Main : Control
{
	[Export]
	Label WarningLabel;
	[Export]
	Button CreateWalletButton;

	[Export]
	Button UseWalletButton;
	[Export]
	Button ImportWalletButton;
	[Export]
	Button DeleteWalletButton;
	
	private void Aviso(string message, string color) {
		WarningLabel.LabelSettings.FontColor = new Color(color);
		WarningLabel.Text = message;
		WarningLabel.Visible = true;

	}

	private void _on_create_wallet_pressed () {

		PackedScene GenerateWallet = ResourceLoader.Load<PackedScene>("res://scenes/GenerateWallet/GenerateWallet.tscn");

		GetTree().ChangeSceneToPacked(GenerateWallet);

	}

}
