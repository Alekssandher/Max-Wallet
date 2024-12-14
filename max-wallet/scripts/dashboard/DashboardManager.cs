using Godot;
using MaxWallet.scripts.dashboard.utils;
using MaxWallet.scripts.PosLog;
using System;

public partial class DashboardManager : Node
{
	[Export]
	Label WarningLabel;
	[Export]
	Label BtcBalance;
	[Export]
	Label UsdBalance;
	[Export]
	Label SatsBalance;

	AddressesInfo AddressInfos;

	Balance balance;

	Wallet walletData = GlobalVars.Instance.fileData;
	
	public class Balance {

		public decimal BTC { get; set; }
		public long SATS { get; set;}
		public double USD { get; set; }
		
	}

	

	
	public override void _Ready()
	{
		GlobalVars.Instance.fileData = null;
		CheckBalance();
	}
	
	public async void CheckBalance() {

		AddressInfos = await GetBlockchainInfos.GetInfos(walletData);
		
		if(AddressInfos.FinalBalance == 0 ) {

			WarningLabel.Text = "Your wallet has no founds!";
			WarningLabel.LabelSettings.FontColor = new Color("bd0a33");
			return;
		} 

		balance = new Balance {
			BTC = AddressInfos.FinalBalance / 100_000_000m,
			SATS = AddressInfos.FinalBalance,
			USD = await ConvertBtc.GetValue("usd", AddressInfos.FinalBalance)
			
		};
		
		BtcBalance.Text = balance.BTC.ToString() + "btc";
		SatsBalance.Text = balance.SATS.ToString() + "sats";
		UsdBalance.Text = balance.USD.ToString() + "usd";
		

	}

	private void _on_reload_pressed() {
		CheckBalance();
	}
}
