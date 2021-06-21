namespace HumbleUtilities
{
    using Nethereum.ABI.FunctionEncoding.Attributes;
    using Nethereum.Contracts;

    [Function("allowance", "uint256")]
    public class GetAllowanceFunction : FunctionMessage
    {
        /// <summary>
        /// The wallet who owns the tokens
        /// </summary>
        [Parameter("address", "owner", 1)]
        public string Owner { get; set; }

        /// <summary>
        /// The smart contract which needs permissions to spend
        /// </summary>
        [Parameter("address", "spender", 2)]
        public string Spender { get; set; }
    }
}