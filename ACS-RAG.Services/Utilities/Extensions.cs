using Microsoft.IdentityModel.Tokens;
using SharpToken;

namespace ACS.RAG.Services.Utilities;

public static class Extensions
{
    public static string DecodeBase64URL(this string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentNullException(nameof(url));

        var decodedPath = Base64UrlEncoder.Decode(url);
        return decodedPath;
    }

    public static int TokensCount(this string text)
    {
        if (string.IsNullOrEmpty(text))
            throw new ArgumentNullException(nameof(text));

        var encoding = GptEncoding.GetEncodingForModel("gpt-3.5-turbo");
        var encoded = encoding.Encode(text);
        return encoded.Count;
    }
}
