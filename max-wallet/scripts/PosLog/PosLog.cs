using Godot;
using MaxWallet.scripts.PosLog;

public partial class PosLog : Control
{
	[Export]
	Label WarningLabel;
	[Export]
	Button CreateWalletButton;
	[Export]
	Button GenerateWalletButton;

	[Export]
	Button UseWalletButton;
	[Export]
	Button ImportWalletButton;
	[Export]
	Button DeleteWalletButton;
	[Export]
	LineEdit LineEdit;
	[Export]
	AnimationPlayer Animation;
	
	CreateWallet createWalletInstance = new CreateWallet();
	
	public override void _Ready()
	{
		LineEdit.Visible = false;
		GenerateWalletButton.Visible = false;
	
	}
	
	public void Aviso(string message, string color) {
		WarningLabel.LabelSettings.FontColor = new Color(color);
		WarningLabel.Text = message;
		WarningLabel.Visible = true;

	}

	private void _on_create_wallet_pressed () {
		UseWalletButton.Visible = false;
		ImportWalletButton.Visible = false;
		DeleteWalletButton.Visible = false;
		CreateWalletButton.Visible = false;

		GenerateWalletButton.Text = "Create";
		GenerateWalletButton.Visible = true;
		LineEdit.Visible = true;
		LineEdit.PlaceholderText = "Your wallet name";
	}

	private void _on_generate_wallet_pressed () {
	
		if (string.IsNullOrWhiteSpace(LineEdit.Text)){
			Aviso("Your wallet name can't be empty", "bd0a33");

			return;
		}

		
		string mnemonicPhrase;

		Wallet wallet = createWalletInstance.Generate(LineEdit.Text, out mnemonicPhrase);
		
		Container container = GetNode<Container>("GeneratedAnim/container");

		container.FillItens("Wallet Generated", "Here's your seed, you'll not see it again, so keep it in a safe place like your  mind.", mnemonicPhrase);
		Animation.Play("WalletCreated");
		

	}
}
