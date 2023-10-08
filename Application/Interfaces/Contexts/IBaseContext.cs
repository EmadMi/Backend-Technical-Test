using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Contexts
{
    public interface IBaseContext
    {
        DbSet<Area> Areas { get; set; }
        DbSet<AreaRule> AreaRules { get; set; }
        DbSet<Vehicle> Vehicles { get; set; }
        DbSet<Holiday> Holidays { get; set; }
        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        EntityEntry Entry(object entity);
    }
}
