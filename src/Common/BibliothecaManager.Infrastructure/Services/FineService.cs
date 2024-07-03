using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BibliothecaManager.Infrastructure.Services;
public class FineService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    public FineService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested) 
        {
            using var scope = _scopeFactory.CreateScope();
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                foreach(var borrow in context.Borrows.Where(x => x.IsReturned == false && x.ReturnDate > DateTime.UtcNow)) 
                {
                    borrow.Fine += 1.5f;
                }
                await context.SaveChangesAsync(stoppingToken);
            }
            await Task.Delay(86400000, stoppingToken);
        }
    }
}
