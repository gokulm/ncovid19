using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoltOn.Bootstrapping;
using Covid19WebApi.Data;
using Covid19WebApi.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Covid19WebApi {
    public class PostRegistrationTask : IPostRegistrationTask {
        private readonly IServiceProvider _serviceProvider;

        public PostRegistrationTask (IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public void Run () {
            using var scope = _serviceProvider.CreateScope ();
            var covid19DbContext = scope.ServiceProvider.GetService<Covid19DbContext> ();
            covid19DbContext.Database.EnsureDeleted ();
            covid19DbContext.Database.EnsureCreated ();

            var victimVisitedLocation = new VictimVisitedLocation {
                Id = Guid.NewGuid (),
                Address = "test address",
                VisitedDate = DateTime.Today.AddDays (-1).ToString ()
            };
            covid19DbContext.Set<VictimVisitedLocation> ().Add (victimVisitedLocation);
            covid19DbContext.SaveChanges ();
        }
    }
}