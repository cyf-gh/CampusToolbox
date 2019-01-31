using CTB.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTB.Service.HHI {
    public interface IHHIService : IService {
        /// <summary>
        /// Get HHI Model
        /// </summary>
        /// <returns>
        /// <see cref="CTB.Model.HHI.HHIModel"/>
        /// </returns>
        IBackModel GetHHIModel();
        /// <summary>
        /// Get HHI User Model
        /// </summary>
        /// <returns>
        /// <see cref="CTB.Model.HHI.HHIUserModel"/>
        /// </returns>
        IModel GetUserModel();
    }
}
