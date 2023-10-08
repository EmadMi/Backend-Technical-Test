using Application.DTOs.Results;
using Application.DTOs.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Vehicles
{
    public interface IVehicleService
    {
        GetVehicleDTO? GetVehicleById(int Id);
        List<GetVehicleDTO> GetVehicles();
        ResultDTO<GetVehicleDTO> CreateVehicle(CreateVehicleDTO model);
        ResultDTO<GetVehicleDTO> UpdateVehicle(UpdateVehicleDTO model);
        ResultDTO DeleteVehicle(DeleteVehicleDTO model);
    }
}
