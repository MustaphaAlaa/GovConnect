using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository.ISFs
{
    /// <summary>
    /// Interface for Scalar Function IsTestTypePassed
    /// </summary>
    public interface ISF_IsTestTypePassed
    {
        /// <summary>
        /// Function check if a specfic test for a specfic local driving appliction is passed or not
        /// </summary>
        /// <param name="LDLApplicationId">The id of the local driving license application.</param>
        /// <param name="TestTypeId">The id of the test type. </param>
        /// <returns>bool if the test is found and passed</returns>
        Task<bool> IsTestTypePassed(int LDLApplicationId, int TestTypeId);
    }
}
