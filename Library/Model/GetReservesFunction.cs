namespace HumbleVerifierLibrary
{
    using Nethereum.ABI.FunctionEncoding.Attributes;
    using Nethereum.Contracts;

    [Function("getReserves", typeof(GetReservesOutputDTO))]
    public class GetReservesFunction : FunctionMessage
    {
    }
}