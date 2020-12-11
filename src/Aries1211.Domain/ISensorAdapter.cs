namespace Aries1211.Domain
{
    public interface ISensorAdapter
    {
        decimal? GetPressure();

        decimal? GetOxygen();

        decimal? GetTemperature();
    }
}
