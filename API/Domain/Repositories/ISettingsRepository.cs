namespace API.Domain.Repositories
{
    public interface ISettingsRepository
    {
        float GetTax(int settingsId);
    }
}
