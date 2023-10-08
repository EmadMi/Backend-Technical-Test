using Application.DTOs.Results;
using Application.DTOs.TaxCalculators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Calculators
{
    public interface ICalculatorService
    {
        ResultDTO<GetTaxCalculatorDTO> TaxCalculator(GetTaxCalculatorDTO model);
    }
}
