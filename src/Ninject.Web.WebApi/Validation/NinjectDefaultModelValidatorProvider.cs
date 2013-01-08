//-------------------------------------------------------------------------------
// <copyright file="NinjectDefaultModelValidatorProvider.cs" company="bbv Software Services AG">
//   Copyright (c) 2012 bbv Software Services AG
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.Metadata;
    using System.Web.Http.Validation;
    using System.Web.Http.Validation.Providers;

    /// <summary>
    /// Provides the validators provided by the default model validator providers.
    /// </summary>
    public class NinjectDefaultModelValidatorProvider : ModelValidatorProvider
    {
        /// <summary>
        /// The ninject kernel.
        /// </summary>
        private readonly IKernel kernel;

        /// <summary>
        /// The default model validator providers.
        /// </summary>
        private readonly IEnumerable<ModelValidatorProvider> defaultModelValidatorProviders;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectDefaultModelValidatorProvider"/> class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        /// <param name="defaultModelValidatorProviders">The default model validator providers.</param>
        public NinjectDefaultModelValidatorProvider(IKernel kernel, IEnumerable<ModelValidatorProvider> defaultModelValidatorProviders)
        {
            this.kernel = kernel;

            this.defaultModelValidatorProviders = defaultModelValidatorProviders
                .Where(p => !(p is DataAnnotationsModelValidatorProvider)).ToList();
            /*
            DataAnnotationsModelValidatorProvider.RegisterDefaultAdapterFactory(
                ((providers, attribute) =>
                    {
                        this.kernel.Inject(attribute);
                        return (ModelValidator)new DataAnnotationsModelValidator(providers, attribute);
                    }));*/
        }

        /// <summary>
        /// Gets the validators.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="validatorProviders">The validator providers.</param>
        /// <returns>
        /// The validators returned by the default validator providers.
        /// </returns>
        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, IEnumerable<ModelValidatorProvider> validatorProviders)
        {
            var validators = this.defaultModelValidatorProviders.SelectMany(provider => provider.GetValidators(metadata, validatorProviders)).ToList();
            foreach (var modelValidator in validators)
            {
                this.kernel.Inject(modelValidator);
            }

            return validators;
        }
    }
}