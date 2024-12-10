using Godot;
using NBitcoin;

namespace MaxWallet.scripts.PosLog
{
    public class CreateWallet
    {
        public string Generate() {

            Network network = Network.Main;
            Mnemonic mnemonic = new Mnemonic(Wordlist.English, WordCount.TwentyOne);
            string mnemonicPhrase = mnemonic.ToString();

            ExtKey masterKey = mnemonic.DeriveExtKey();

            BitcoinSecret privateKey = masterKey.PrivateKey.GetWif(network);
            PubKey publicKey = masterKey.PrivateKey.PubKey;
            BitcoinAddress address = publicKey.GetAddress(ScriptPubKeyType.Segwit, network);

            GD.Print("Address: " + address);
            GD.Print("Private key: " + privateKey);
            GD.Print("Seed: ", mnemonicPhrase);

            return "";
        }
    }
}