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
    using System.Web.Http.Controllers;
    using System.Web.Http.Metadata;
    using System.Web.Http.Validation;
    using System.Web.Http.Validation.Providers;
    using System.Web.Http.Validation.Validators;

    public class NinjectDefaultModelValidatorProvider : ModelValidatorProvider
    {
        private readonly IKernel kernel;
        private readonly IEnumerable<ModelValidatorProvider> defaultModelValidators;

        public NinjectDefaultModelValidatorProvider(IKernel kernel, IEnumerable<ModelValidatorProvider> defaultModelValidators)
        {
            this.kernel = kernel;
            this.defaultModelValidators = defaultModelValidators.ToList();
            DataAnnotationsModelValidatorProvider.RegisterDefaultAdapterFactory(
                ((metadata, context, attribute) =>
                    {
                        this.kernel.Inject(attribute);
                        return (ModelValidator)new DataAnnotationsModelValidator(metadata, context, attribute);
                    }));
        }

        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, HttpActionContext actionContext)
        {
            var validators = this.defaultModelValidators.SelectMany(provider => provider.GetValidators(metadata, actionContext)).ToList();
            foreach (var modelValidator in validators)
            {
                this.kernel.Inject(modelValidator);
            }

            return validators;
        }
    }
}