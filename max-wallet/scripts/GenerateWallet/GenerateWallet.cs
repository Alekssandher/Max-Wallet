using Godot;
using MaxWallet.scripts.PosLog;
using MaxWallet.scripts.utils;
using System;
using System.IO;

public partial class GenerateWallet : Node
{

	[Export]
	LineEdit WalletName;
	[Export]
	LineEdit WalletPassword;
	[Export]
	Label WarningLabel;
	[Export]
	CanvasLayer Canvas;

	CreateWallet createWalletInstance = new CreateWallet();
	PackedScene AnimationScene = GD.Load<PackedScene>("res://scenes/GeneratedAnim/generated_anim.tscn");

	private void Aviso(string message, string color) {
		WarningLabel.LabelSettings.FontColor = new Color(color);
		WarningLabel.Text = message;
		WarningLabel.Visible = true;

	}

	private void _on_generate_wallet_button_pressed() {

		if (string.IsNullOrWhiteSpace(WalletName.Text)){
			Aviso("Your wallet name can't be empty", "bd0a33");

			return;
		}

		if (string.IsNullOrWhiteSpace(WalletPassword.Text)){
			Aviso("Your wallet password can't be empty", "bd0a33");

			return;
		}

		string walletDirectory = Path.Combine(OS.GetUserDataDir(), "wallets");
        string walletPath = Path.Combine(walletDirectory, WalletName.Text + ".txt");

		if (File.Exists(walletPath)) {
			Aviso("A wallet with this name already exists. Please choose a different name.", "bd0a33");
			return;
    	}

		string mnemonicPhrase;

		Wallet wallet = createWalletInstance.Generate(WalletName.Text, out mnemonicPhrase);
		
		string walletData = wallet.ToString();

		byte[] data = Encrypt.EncryptData(walletData, WalletPassword.Text);

		string directoryPath = Path.GetDirectoryName(walletPath);

		if (!Directory.Exists(directoryPath)) {
			Directory.CreateDirectory(directoryPath);
		}


		File.WriteAllBytes(walletPath, data);

		var Animation = AnimationScene.Instantiate<AnimationPlayer>();
		Canvas.AddChild(Animation);

		var container = Animation.GetNode<Container>("container");
		
		container.FillItens("Wallet Generated", "Here's your seed, you'll not see it again, so keep it in a safe place like your  mind.", mnemonicPhrase);
		
		

		GlobalVars.Instance.fileData = wallet;

		Animation.Play("WalletCreated");

	}

	
}
