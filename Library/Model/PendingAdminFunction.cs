namespace HumbleVerifierLibrary
{
    using Nethereum.ABI.FunctionEncoding.Attributes;
    using Nethereum.Contracts;

    [Function("pendingAdmin", "address")]
    public class PendingAdminFunction : FunctionMessage
    {
    }
}