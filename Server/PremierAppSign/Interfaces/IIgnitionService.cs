using PremierAppSign.Models.Ignition;

namespace PremierAppSign.Interfaces
{
    public interface IIgnitionService
    {
        object GetConfig();

        string SignIn(SignInRequest request);
    }
}
