namespace BestService.Services.StringHelpers
{
    public static class StringManipulations
    {
        public static string GetNameFromUriWithoutExtension(string uri)
        {
            int indexOfDot = uri.LastIndexOf(".");
            int indexOfLastSlash = uri.LastIndexOf("/") + 1;
            int lenght = indexOfDot - indexOfLastSlash;

            if (lenght < 0)
            {
                return null;
            }

            var nameWithoutExt = uri.Substring(indexOfLastSlash, lenght);
            return nameWithoutExt;
        }
    }
}
