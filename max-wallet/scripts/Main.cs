using Godot;
using System;
using NBitcoin;
using System.Security.Cryptography.X509Certificates;
using BCrypt.Net;
using System.IO;
using Newtonsoft.Json;

public partial class Main : Node
{	
	private Button _createWalletButton;
	public string inputPassword;
	public string savePath = "saves/passInfo.json";
	public int cost = 16;

	[Export]
	public LineEdit UserInput { get; set; }
	[Export]
	public Label Message {get; set;}
	[Export]
	public Button ActionButton { get; set;}
	[Export]
	public Label WarningLabel;
	

	public override void _Ready()
	{	
		WarningLabel.Visible = false;

		if (File.Exists(savePath)) {
			Message.Text = "Type your password";
			ActionButton.Text = "Login";

		}
		else {
			Message.Text = "Create your password";
			ActionButton.Text = "Register password";

		}
		
	}

	public class PasswordInfo {
		public string Salt { get; set; }
		public string PasswordHash { get; set; }
	}
	private void _on_action_button_pressed() {

		if (File.Exists(savePath)) {
			Login();
		}
		else {
			Cadastro();
		}

	}

	public void Aviso(string message, string color) {
		WarningLabel.LabelSettings.FontColor = new Color(color);
		WarningLabel.Text = message;
		WarningLabel.Visible = true;

	}
	
	public object Encript(string inputPassword) {

		var sw = System.Diagnostics.Stopwatch.StartNew();

		string salt = BCrypt.Net.BCrypt.GenerateSalt(16);		
		
		string passwordHash =  BCrypt.Net.BCrypt.HashPassword(inputPassword + salt, workFactor: cost);
		sw.Stop();

		GD.Print("Levou " + sw.ElapsedMilliseconds + "ms");

		var passInfo = new PasswordInfo {Salt = salt, PasswordHash = passwordHash};

		return passInfo;
	}
	public void Cadastro() {

		if(UserInput.Text == ""){ Aviso("Your password can't be empty", "bd0a33"); }
		
		inputPassword = UserInput.Text;

		object passInfo = Encript(inputPassword);

		string json = JsonConvert.SerializeObject(passInfo);

		File.WriteAllText(savePath, json);
		
		Aviso("Password successfuly registred", "5db900");

		UserInput.Text = "";

	}

	public void Login() {
		string json = File.ReadAllText(savePath);

		PackedScene posLog = ResourceLoader.Load<PackedScene>("res://scenes/PosLog.tscn");

		PasswordInfo passInfo = JsonConvert.DeserializeObject<PasswordInfo>(json);

		if (BCrypt.Net.BCrypt.Verify(UserInput.Text + passInfo.Salt, passInfo.PasswordHash)) {
			Aviso("Loged successfuly", "5db900");

			GetTree().ChangeSceneToPacked(posLog);

			GD.Print("Logado");
		}
		else {
			Aviso("Wrong password, try again", "bd0a33");
			UserInput.Text = "";
		}
	}
}
