using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace DataAccessLibrary.Helpers
{
    /// <summary>
    /// Simple mapping helpers used for mapping model results
    /// </summary>
    public static class MappingHelpers
    {
        /// <summary>
        /// Mapping from one model to another 
        /// </summary>
        /// <typeparam name="NM">The type of the new model</typeparam>
        /// <typeparam name="OM">The type of the old model</typeparam>
        /// <param name="newModel">The new model</param>
        /// <param name="oldModel">The old model</param>
        /// <param name="propertiesToSkip">Name of the properties to skip, if any</param>
        /// <returns></returns>
        /// <remarks>Useful when mapping to viewmodels etc</remarks>
        public static NM MapModel<NM, OM>(this NM newModel, OM oldModel, params string[] propertiesToSkip)
            where NM : class, new()
        {
            // Get the properties
            var newModelProperties = newModel.GetType().GetProperties();
            var oldModelProperties = newModel.GetType().GetProperties();

            // Go through all the properties of the model which holds the values
            Parallel.ForEach(oldModelProperties, (prop) =>
            {
                // Check for the correct property to set the value to
                foreach(var newProp in newModelProperties)
                {
                    // If the name matches and the property should not be skipped...
                    if (prop.Name.Equals(newProp.Name) && !propertiesToSkip.Contains(prop.Name))
                    {
                        // ...Set the value
                        newProp.SetValue(newModel, prop.GetValue(oldModel));
                    }
                }
            });

            return newModel;
        }
    }
}
