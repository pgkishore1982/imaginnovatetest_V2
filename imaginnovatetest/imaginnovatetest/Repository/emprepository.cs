using imaginnovatetest.Models;
using imaginnovatetest.Models.DTO;
using imaginnovatetest.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Xml.Linq;
using System;

namespace imaginnovatetest.Repository
{
    public class emprepository : Iemployeedetails
    {
        private readonly ImaginnovateDbContext _dbcontext;
        private readonly ILogger<emprepository> _logger;
        private readonly IMapper _mapper;

        public emprepository(ImaginnovateDbContext dbcontext, ILogger<emprepository> logger, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _logger = logger;
            _mapper = mapper;
        }


        /// <summary>
        /// Save employee details
        /// </summary>
        /// <param name="objemployeeDto"></param>
        /// <returns></returns>
        public async Task<string> Saveemployee(employeeDto objemployeeDto)
        {
            try
            {

                TblEmployee objemp = _mapper.Map<TblEmployee>(objemployeeDto);
                _dbcontext.TblEmployees.Add(objemp);
                await _dbcontext.SaveChangesAsync();
                return "Date Saved Successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Get employee yearly tax details based on financial year range
        /// </summary>
        /// <param name="financialyearDto"></param>
        /// <returns></returns>
        public async Task<List<emptaxdetailsDto>> GetEmptaxdetails(financialyearDto financialyearDto)
        {
            try
            {
                var employees = await (from e in _dbcontext.TblEmployees
                                       select new 
                                       {
                                         empid = e.Empid,
                                         firstname = e.Firstname,
                                         lastname = e.Lastname,
                                         doj = e.Doj,
                                         salary = e.Salary
                                         }).ToListAsync();

                List<emptaxdetailsDto> emptaxresult = new List<emptaxdetailsDto>();
                for (int i = 0; i < employees.Count(); i++)
                {
                    emptaxdetailsDto result = new emptaxdetailsDto();
                    var dt = employees[i].doj ?? DateTime.Now;
                    result.employeeid = employees[i].empid;
                    result.firstname = employees[i].firstname;
                    result.lastname = employees[i].lastname;
                    result.yearlysalary = yearlysalary_basedOn_DOJ(Convert.ToInt32(financialyearDto.financialyear), employees[i].salary ?? 0, dt);
                    if (result.yearlysalary > 250000)
                    {
                        if (result.yearlysalary > 250000)
                            if (result.yearlysalary <= 500000)
                                result.taxamount = (yearlysalary_basedOn_DOJ(Convert.ToInt32(financialyearDto.financialyear), employees[i].salary ?? 0, dt) - 250000) / 100 * 5;
                            else result.taxamount = 250000 / 100 * 5;
                        if (result.yearlysalary > 500000)
                            if (result.yearlysalary <= 1000000)
                                result.taxamount = (yearlysalary_basedOn_DOJ(Convert.ToInt32(financialyearDto.financialyear), employees[i].salary ?? 0, dt) - 500000) / 100 * 10;
                            else result.taxamount += 250000 / 100 * 10;
                                     if (result.yearlysalary > 1000000)
                            result.taxamount += 250000 / 100 * 20;
                       if (result.yearlysalary > 2500000)
                            result.taxamount += result.cessamount = (result.yearlysalary - 250000) / 100 * 2;
                       if (result.taxamount > 0)
                            result.taxamount = decimal.Round(result.taxamount ?? 0, 2);
                        if (result.cessamount > 0)
                            result.cessamount = decimal.Round(result.cessamount ?? 0, 2);
                   }
                    emptaxresult.Add(result);
              }

                return emptaxresult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Caliculate based on date of join caliculate yearly salary
        /// </summary>
        /// <param name="financialYear"></param>
        /// <param name="SalaryPerMonth"></param>
        /// <param name="DateOfJoin"></param>
        /// <returns></returns>
        public decimal yearlysalary_basedOn_DOJ(int financialYear, decimal SalaryPerMonth, DateTime DateOfJoin)
        {
            var ysVal = SalaryPerMonth * 12;
            if (DateTime.Now.Year == DateOfJoin.Year && DateOfJoin.Month >= 4 && DateOfJoin.Day > 1)
            {
                var noTaxMonth = DateOfJoin.Month - 3;
                var daysOfMonth = DateTime.DaysInMonth(financialYear, DateOfJoin.Month);
                var noTaxDays = daysOfMonth - DateOfJoin.Day;
                if (noTaxMonth > 0 && noTaxDays == 0)
                    ysVal -= (SalaryPerMonth * noTaxMonth);
                else
                {
                    ysVal -= SalaryPerMonth * (noTaxMonth - 1);
                    ysVal -= (SalaryPerMonth / daysOfMonth) * noTaxDays;
                }
            }
            return ysVal;
        }

    }

}
