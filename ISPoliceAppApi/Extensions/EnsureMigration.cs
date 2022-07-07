using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ISPoliceAppApi.Extensions
{
  public static class EnsureMigration
  {
    public static void EnsureMigrationOfContext<T>(this IApplicationBuilder app) where T : DbContext
    {
      var context = app.ApplicationServices.GetService<T>();
      context.Database.EnsureCreated();
      context.Database.Migrate();
    }
  }
}
