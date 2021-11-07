
namespace restaurante_tech_api.Services.Interfaces
{
    public interface ICryptographyService
    {
        public string GetEncodedString(string rawString);
        public bool VerifyEncodedString(string rawString, string encodedString);
    }
}
