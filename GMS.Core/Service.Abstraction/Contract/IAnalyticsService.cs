using Shared.DTOs.AnalyticsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction.Contract {
    public interface IAnalyticsService {
        Task<AnalyticDTO> GetAnalyticData();
    }
}
