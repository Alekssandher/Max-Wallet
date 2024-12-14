using Godot;
using MaxWallet.scripts.PosLog;
using System;

public partial class GlobalVars : Node
{	
	public static GlobalVars Instance { get; private set; }

	[Signal]
	public delegate void WalletCreatedSignalEventHandler(byte[] fileData);
	//Use example
	// GlobalVars.Instance.WalletCreatedSignal += OnWalletCreated
	public Wallet fileData;
	public override void _Ready()
	{
		Instance = this;
	}

	
}
