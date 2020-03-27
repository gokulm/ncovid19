using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Covid19WebApi.Data;
using Covid19WebApi.DTOs;
using Covid19WebApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Covid19WebApi.Controllers
{
    public class VictimController : Controller
    {
        private readonly Covid19DbContext _covid19DbContext;
        private readonly LatitudeLongitudeService _latitudeLongitudeService;

        public VictimController(Covid19DbContext covid19DbContext,
            LatitudeLongitudeService latitudeLongitudeService)
        {
            _covid19DbContext = covid19DbContext;
            _latitudeLongitudeService = latitudeLongitudeService;
        }

        [HttpPost, Route("[controller]/locations")]
        public async Task<string> AddLocations([FromBody]AddVictimLocationsRequest request)
        {
            foreach (var location in request.Locations)
            {
                location.Id = Guid.NewGuid();
                var latLong = await _latitudeLongitudeService.Get(location.Address);
                if (latLong != null)
                {
                    location.Latitude = latLong.lat;
                    location.Longitude = latLong.lon; 
                }
                await _covid19DbContext.Set<VictimVisitedLocation>().AddAsync(location);
            }

            await _covid19DbContext.SaveChangesAsync();
            return await Task.FromResult("success");
        }

        [HttpPost, Route("[controller]/visitedlocations")]
        public async Task<IEnumerable<VictimVisitedLocation>> GetVictimLocations([FromBody]VictimLocationsRequest request)
        {
            var latLong = await _latitudeLongitudeService.Get(request.CurrentLocation);

            if (latLong == null) return new List<VictimVisitedLocation>();

            var sql = $"DECLARE @g geography = 'Point({latLong.lon} {latLong.lat})' " +
                      "SELECT  " +
                      "Round(SpatialColumn.STDistance(@g) * 0.0006, 1) as locationDistance, " +
                      "SpatialColumn.ToString() as location, " +
                      "[VictimVisitedLocationId]," +
                      "[Address], " +
                      "visitedDate ," +
                      "Latitude ," +
                      "Longitude " +
                      "FROM[dbo].VictimVisitedLocation " +
                      $"WHERE SpatialColumn.STDistance(@g) IS NOT NULL and(SpatialColumn.STDistance(@g) * 0.0006) < {request.Radius} " +
                      "and DATEDIFF(ww,visitedDate, Getdate()) <= 2 " +
                      "ORDER BY SpatialColumn.STDistance(@g);";

            var victimVistedLocations = _covid19DbContext.Set<VictimVisitedLocation>().FromSqlRaw(sql);

            return victimVistedLocations;
        }
    }
}