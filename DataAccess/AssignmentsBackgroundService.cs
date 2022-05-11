using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AssignmentsBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public AssignmentsBackgroundService(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
               await CallAssignmentService();
                await Task.Delay(TimeSpan.FromMinutes(10));
            }
        }

        private async Task CallAssignmentService()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var assignmentService = scope.ServiceProvider.GetRequiredService<IAssignmentService>();
                await assignmentService.ArchiveAssignmentsAsync();
            }
        }
    }
}
