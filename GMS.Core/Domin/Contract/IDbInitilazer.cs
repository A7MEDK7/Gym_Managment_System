using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Contract {
    public interface IDbInitilazer {
        public Task InitilazeAsync();
    }
}
