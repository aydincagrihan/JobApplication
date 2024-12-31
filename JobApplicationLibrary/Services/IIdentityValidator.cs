using JobApplicationLibrary.Models;

namespace JobApplicationLibrary.Services
{
    public interface IIdentityValidator
    {
        bool IsValid(string identityNumber);

        bool CheckConnectionToRemoteServer();
        ICountryDataProvider CountryDataProvider { get; }
        public ValidationStatus ValidationStatus { get; set; }


    }

    public interface ICountryData
    {
        string Country { get; }

    }

    public interface ICountryDataProvider
    {
        ICountryData CountryData { get; }
    }

    public enum ValidationStatus
    {
        Detailed,
        Quick
    }
}