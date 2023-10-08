using Application.DTOs.AreaRules;
using Application.DTOs.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.AreaRules
{
    public interface IAreaRuleService
    {
        GetAreaRuleDTO? GetAreaRuleById(int Id);
        List<GetAreaRuleDTO> GetAreaRules();
        List<GetAreaRuleDTO> GetAreaRules(int AreaId);
        ResultDTO<GetAreaRuleDTO> CreateAreaRule(CreateAreaRuleDTO model);
        ResultDTO<GetAreaRuleDTO> UpdateAreaRule(UpdateAreaRuleDTO model);
        ResultDTO DeleteAreaRule(DeleteAreaRuleDTO model);
    }
}
