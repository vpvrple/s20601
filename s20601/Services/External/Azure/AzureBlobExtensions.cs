namespace s20601.Services.External.Azure;

public static class AzureBlobExtensions
{
    public static string GetContainerName(this AzureBlobType type) => type switch
    {
        AzureBlobType.MovieImages => "moviedetailsimages",
        AzureBlobType.UserAvatars => "useravatars",
        AzureBlobType.MovieCollectionImages => "moviecollectionimages",
        _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
    };
}