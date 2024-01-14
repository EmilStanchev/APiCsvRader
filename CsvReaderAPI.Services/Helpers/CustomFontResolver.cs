using PdfSharp.Fonts;

namespace CsvReader.API.Helpers
{
    public class CustomFontResolver: IFontResolver
    {
        public byte[] GetFont(string faceName)
        {
            string fontPath = @"C:\Windows\Fonts\arial.ttf";

            using (var ms = new MemoryStream())
            {
                using (var fontFileStream = new FileStream(fontPath, FileMode.Open, FileAccess.Read))
                {
                    fontFileStream.CopyTo(ms);
                    return ms.ToArray();
                }
            }
        }

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            return new FontResolverInfo("Arial", isBold, isItalic);
        }
    }
}
