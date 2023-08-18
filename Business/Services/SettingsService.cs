using Business.Model;
using System;

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
