
using MediatR;
using Domain;
using Persistence;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;


namespace Application
{
    public class Query : IRequest<List<Activity>> { }

    public class ListQueryHandler : IRequestHandler<Query, List<Activity>>
    {
        private DataContext _context;
        private ILogger _logger;
        public ListQueryHandler(DataContext context, ILogger<ListQueryHandler> ilogger)
        {
            _context = context;
            _logger = ilogger;
        }
        public async Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    await Task.Delay(1000, cancellationToken);
                    _logger.LogInformation($"Task {i} has completed.");
                }
            }
            catch (System.Exception)
            {
                _logger.LogInformation("Task was cancelled.");
            }

            return await _context.Activities.ToListAsync();
        }
    }
}