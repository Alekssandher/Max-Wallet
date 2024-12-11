
using NBitcoin;

namespace MaxWallet.scripts.PosLog
{   

    public class Wallet {
        public BitcoinAddress Address { get; set; }
        public BitcoinSecret PrivateKey { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"Wallet Name: {Name}, Address: {Address}, Private Key: {PrivateKey}";
        }
    }
    
    
    public class CreateWallet
    {
        public Wallet Generate(string name, out string mnemonicPhrase) {

            Network network = Network.Main;
            Mnemonic mnemonic = new Mnemonic(Wordlist.English, WordCount.TwentyOne);
            mnemonicPhrase = mnemonic.ToString();

            ExtKey masterKey = mnemonic.DeriveExtKey();

            BitcoinSecret privateKey = masterKey.PrivateKey.GetWif(network);
            PubKey publicKey = masterKey.PrivateKey.PubKey;
            BitcoinAddress address = publicKey.GetAddress(ScriptPubKeyType.Segwit, network);

            Wallet wallet = new Wallet{
                Address = address,
                PrivateKey = privateKey,
                Name = name
            };

            return wallet;
        }
    }
}