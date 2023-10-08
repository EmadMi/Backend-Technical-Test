using Application.DTOs.Areas;
using Application.DTOs.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Areas
{
    public interface IAreaService
    {
        GetAreaDTO? GetAreaById(int Id);

        List<GetAreaDTO> GetAreas();

        ResultDTO<GetAreaDTO> CreateArea(CreateAreaDTO model);
        ResultDTO<GetAreaDTO> UpdateArea(UpdateAreaDTO model);
        ResultDTO DeleteArea(DeleteAreaDTO model);
    }
}
