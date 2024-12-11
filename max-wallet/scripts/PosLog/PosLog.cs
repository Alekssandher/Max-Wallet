using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text;
using Godot;
using MaxWallet.scripts.PosLog;
using MaxWallet.scripts.utils;

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
	LineEdit WalletPassword;
	[Export]
	AnimationPlayer Animation;
	
	CreateWallet createWalletInstance = new CreateWallet();
	
	public override void _Ready()
	{
		LineEdit.Visible = false;
		GenerateWalletButton.Visible = false;
		WalletPassword.Visible = false;
	
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
		WalletPassword.Visible = true;
		LineEdit.PlaceholderText = "Your wallet name";
	}

	private void _on_generate_wallet_pressed () {
	
		if (string.IsNullOrWhiteSpace(LineEdit.Text)){
			Aviso("Your wallet name can't be empty", "bd0a33");

			return;
		}

		string walletPath = "../wallets/" + LineEdit.Text + ".txt";

		string mnemonicPhrase;

		Wallet wallet = createWalletInstance.Generate(LineEdit.Text, out mnemonicPhrase);
		
		string walletData = wallet.ToString();

		byte[] data = Encrypt.EncryptData(walletData, WalletPassword.Text);

		string directoryPath = Path.GetDirectoryName(walletPath);

        if (!Directory.Exists(directoryPath)) {
            Directory.CreateDirectory(directoryPath);
        }

		File.WriteAllBytes(walletPath, data);

		Container container = GetNode<Container>("GeneratedAnim/container");

		container.FillItens("Wallet Generated", "Here's your seed, you'll not see it again, so keep it in a safe place like your  mind.", mnemonicPhrase);
		Animation.Play("WalletCreated");
		

		byte[] fileData = File.ReadAllBytes(walletPath); 
		string decryptedData = Decrypt.DecryptWalletData(fileData, WalletPassword.Text); 
		GD.Print(decryptedData); 
	}
}
