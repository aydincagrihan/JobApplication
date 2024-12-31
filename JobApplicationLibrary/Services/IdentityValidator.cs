using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationLibrary.Services
{
    public class IdentityValidator : IIdentityValidator
    {
        public string Country => throw new NotImplementedException();

        public ICountryDataProvider CountryDataProvider => throw new NotImplementedException();

        public ValidationStatus ValidationStatus { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsValid(string identityNumber)
        {
            return true;
        }

        public bool CheckConnectionToRemoteServer()
        {
            throw new InvalidOperationException("Remote server connection failed");
        }
    }
}
