using System.Threading.Tasks;
using Thirdweb;
using UnityEngine;

namespace RPGM.Core
{
    public class Web3 : MonoBehaviour
    {
        // Instantiate the SDK with Gasless Transactions
        private ThirdwebSDK
            sdk =
                new ThirdwebSDK("https://rpc.testnet.mantle.xyz",
                    new ThirdwebSDK.Options()
                    {
                        gasless =
                            new ThirdwebSDK.GaslessOptions()
                            {
                                openzeppelin =
                                    new ThirdwebSDK.OZDefenderOptions()
                                    {
                                        relayerUrl =
                                            "https://api.defender.openzeppelin.com/autotasks/c2e9a6ca-f2e8-4521-926b-1f9daec2dcb8/runs/webhook/826a5b67-d55d-49dc-8651-5db958ba22b2/DPtceJtayVGgKSDejaFnWk"
                                    }
                            }
                    });

        public async Task<bool> IsConnected()
        {
            return await sdk.wallet.IsConnected();
        }

        public async Task<string> Connect()
        {
            string addr =
                await sdk
                    .wallet
                    .Connect(new WalletConnection()
                    {
                        provider = WalletProvider.CoinbaseWallet, // Use Coinbase Wallet
                        chainId = 5001 // Switch the wallet Mantle testnet network on connection
                    });
            return addr;
        }

        public Contract GetTokenDropContract()
        {
            return sdk
                .GetContract("0x0B844d398F3a19f41209006D1D735FF379d99BDD");
                // MMQ COIN
        }

        public async Task<TransactionResult> Claim()
        {
            await Connect();
            var contract = GetTokenDropContract();
            return await contract.ERC20.Claim("10");
        }

        public Marketplace GetMarketplaceContract()
        {
            return sdk
                .GetContract("0x424832c400e69230E3553Aed59e0eF9A605Ea195")
                .marketplace;
                // MMQ Marketplace
        }

        public async Task<TransactionResult> BuyItem(string itemId)
        {
            await Connect();
            return await GetMarketplaceContract().BuyListing(itemId, 1);
        }

        internal async Task<string> GetAddress()
        {
            if (Application.isEditor)
            {
                return "0x0000000000000000000000000000000000000000";
            }
            return await sdk.wallet.GetAddress();
        }
    }
}
