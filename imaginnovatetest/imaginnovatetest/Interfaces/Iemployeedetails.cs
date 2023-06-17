using imaginnovatetest.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace imaginnovatetest.Interfaces
{
    public interface Iemployeedetails
    {
        Task<string> Saveemployee(employeeDto objemployeeDto);

        Task<List<emptaxdetailsDto>> GetEmptaxdetails(financialyearDto financialyearDto);
    }
}
