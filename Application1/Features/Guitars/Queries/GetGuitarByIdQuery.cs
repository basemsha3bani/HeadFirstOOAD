using Application1.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application1.Features.Guitars.Queries
{
    public class GetGuitarByIdQuery : IRequest<GuitarViewModel>
    {
        public GetGuitarByIdQuery(GuitarViewModel searchCriteria)
        {
            this.searchCriteria = searchCriteria;
        }

        public GuitarViewModel searchCriteria { get; set; } = new GuitarViewModel();
    }
}
