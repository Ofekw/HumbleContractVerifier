namespace HumbleVerifierLibrary
{
    using System.Text.RegularExpressions;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class Deserializer
    {
        public static JToken SafeUnencodeJson(string fullContract, out ContractType contractType)
        {
            JToken result = JToken.FromObject(fullContract);
            try
            {
                result = UnencodeJson(fullContract, out contractType) ?? result;
            }
            catch
            {
                contractType = ContractType.Single;
            }

            return result;
        }

        private static JToken UnencodeJson(string fullContract, out ContractType contractType)
        {
            string trimmedFull = fullContract;
            if (fullContract.StartsWith("{{") && fullContract.EndsWith("}}"))
            {
                trimmedFull = fullContract.Substring(1, fullContract.Length - 2);
            }

            string unescaped = Regex.Unescape(trimmedFull);

            JToken result;
            try
            {
                result = JsonConvert.DeserializeObject<JToken>(unescaped);
            }
            catch
            {
                result = JsonConvert.DeserializeObject<JToken>(trimmedFull);
            }

            // If we failed to parse, we are an all-in-one contract by default
            contractType = ContractType.Single;

            JToken sourcesToken = null;
            if (result != null)
            {
                contractType = ContractType.Multi;
                sourcesToken = result["sources"];
            }

            return sourcesToken ?? result;
        }
    }
}