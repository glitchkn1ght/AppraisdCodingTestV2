using Business.Model;

namespace Business.Services
{
    public interface ISettingsService
    {
        Settings Settings { get; }
    }
    public class SettingsService : ISettingsService
    {
        public Settings Settings => new Settings {};
    }
}
