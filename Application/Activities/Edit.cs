using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command:IRequest
        {
            public Activity Activity{get;set;}
        }   

        public class Handler : IRequestHandler<Edit.Command>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper _mapper;

            public Handler(DataContext dataContext, IMapper mapper)
            {
                _dataContext = dataContext;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(Edit.Command request, CancellationToken cancellationToken)
            {
                var activity = await _dataContext.Activities.FindAsync(request.Activity.Id);
                Console.WriteLine(request.Activity.Title);
                _mapper.Map(request.Activity,activity);
                Console.WriteLine(activity.Title);
                await _dataContext.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}