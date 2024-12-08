using Godot;
using System;
using NBitcoin;
using System.Security.Cryptography.X509Certificates;

public partial class Main : Node
{	
	private Button _createWalletButton;
	
	public string storedPassword = "";

	[Export]
	public LineEdit UserInput { get; set; }

	[Export]
	public Button ActionButton { get; set;}
	[Export]
	public Label WarningLabel;

	public override void _Ready()
	{	
		WarningLabel.Visible = false;

		if(storedPassword == "") {
			ActionButton.Text = "Register password";

		} 
		else {
			ActionButton.Text = "Login";
		}
		
	}

	private void _on_action_button_pressed() {

		if (storedPassword == "") {
			Cadastro();

		}
		else {
			Login();
		}


		// NBitcoin.Key privateKey = new NBitcoin.Key();

		// PubKey publicKey = privateKey.PubKey;

		// var publicKeyHash = publicKey.Hash;

		// var mainNetAddress = publicKeyHash.GetAddress(Network.Main);

		// GD.Print("pressed: " + publicKey);
	}

	public void Aviso(string message, string color) {
		WarningLabel.LabelSettings.FontColor = new Color(color);
		WarningLabel.Text = message;
		WarningLabel.Visible = true;

	}
	
	public void Cadastro() {
		if(UserInput.Text == ""){ 
				Aviso("Your password can't be empty", "bd0a33");

			}

			storedPassword = UserInput.Text;

			Aviso("Password successfuly registred", "5db900");
			UserInput.Text = "";
	}

	public void Login() {
		if(UserInput.Text == storedPassword) {
			GD.Print("loged");
		}
	}
}
