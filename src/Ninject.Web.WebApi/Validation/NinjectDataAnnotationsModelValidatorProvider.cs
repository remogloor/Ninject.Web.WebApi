//-------------------------------------------------------------------------------
// <copyright file="NinjectDataAnnotationsModelValidatorProvider.cs" company="bbv Software Services AG">
//   Copyright (c) 2012 bbv Software Services AG
//   Author: Remo Gloor (remo.gloor@gmail.com)
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-------------------------------------------------------------------------------

namespace Ninject.Web.WebApi.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web.Http.Metadata;
    using System.Web.Http.Validation;
    using System.Web.Http.Validation.Providers;
    using System.Web.Http.Validation.Validators;

    /// <summary>
    /// A <see cref="DataAnnotationsModelValidatorProvider"/> implementation that injects the validators. 
    /// </summary>
    public class NinjectDataAnnotationsModelValidatorProvider : DataAnnotationsModelValidatorProvider
    {
        /// <summary>
        /// The kernel.
        /// </summary>
        private readonly IKernel kernel;

        /// <summary>
        /// The method info to get the attribute from the <see cref="DataAnnotationsModelValidatorProvider"/>
        /// </summary>
        private readonly MethodInfo getAttributeMethodInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectDataAnnotationsModelValidatorProvider"/> class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        public NinjectDataAnnotationsModelValidatorProvider(IKernel kernel)
        {
            this.kernel = kernel;
            this.getAttributeMethodInfo =
                typeof(DataAnnotationsModelValidator).GetMethod("get_Attribute", BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Instance);
        }

        /// <summary>
        /// Gets a list of validators.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="validatorProviders">The validator providers.</param>
        /// <param name="attributes">The list of validation attributes.</param>
        /// <returns>A list of validators.</returns>
        protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, IEnumerable<ModelValidatorProvider> validatorProviders, IEnumerable<Attribute> attributes)
        {
            var validators = base.GetValidators(metadata, validatorProviders, attributes).ToList();
            foreach (var modelValidator in validators.OfType<DataAnnotationsModelValidator>())
            {
                var attribute = this.getAttributeMethodInfo.Invoke(modelValidator, new object[0]);
                this.kernel.Inject(attribute);
            }

            return validators;
        }
    }
}
