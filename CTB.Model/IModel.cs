using System;
using System.Collections.Generic;
using System.Text;

namespace CTB.Model {
    /// <summary>
    /// If model's ViewModel and BackModel is one model, implement this.
    /// </summary>
    public interface IModel {
    }
    public interface IViewModel : IModel {
        IBackModel ToBackModel();
    }
    public interface IBackModel : IModel {
        IViewModel ToViewModel();
    }
}
