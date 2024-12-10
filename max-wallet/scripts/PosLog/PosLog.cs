using Godot;
using MaxWallet.scripts.PosLog;

public partial class PosLog : Control
{
	[Export]
	Button CreateWalletButton;
	[Export]
	Button UseWalletButton;
	[Export]
	Button ImportWalletButton;
	[Export]
	Button DeleteWalletButton;

	CreateWallet createWalletInstance = new CreateWallet();
	
	public override void _Ready()
	{
	}

	private void _on_create_wallet_pressed () {
		createWalletInstance.Generate();
	}
}
