using System.Net;
using Newtonsoft.Json;
using TransactionManager.Application.Exceptions;
using TransactionManager.Application.Interfaces;

namespace TransactionManager.Application.Services.LocationService;

public class LocationService : ILocationService
{
    private readonly string _ipInfoToken;

    public LocationService(string ipInfoToken)
    {
        _ipInfoToken = ipInfoToken;
    }

    public string GetLocationCoordinatesByIp()
    {
        var ipInfo = new IpInfo();

        try
        {
            string url = "https://ipinfo.io?token=" + _ipInfoToken;
            var info = new WebClient().DownloadString(url);
            ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
        }
        catch (Exception ex)
        {
            throw new GetLocationCoordinatesByIpException("Error during getting location coordinates by IP.");
        }

        return ipInfo.Location;
    }
}