using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Route.ValidationManagement
{
    public class ObjectModelValidator : IObjectModelValidator
    {
        private readonly IModelMetadataProvider modelMetadataProvider;
        private readonly ValidatorCache validatorCache;
        private readonly CompositeModelValidatorProvider validatorProvider;

        public ObjectModelValidator(
            IModelMetadataProvider modelMetadataProvider,
            IList<IModelValidatorProvider> validatorProviders)
        {
            if (validatorProviders == null)
                throw new ArgumentNullException(nameof(validatorProviders));
            validatorProvider = new CompositeModelValidatorProvider(validatorProviders);

            validatorCache = new ValidatorCache();

            this.modelMetadataProvider = modelMetadataProvider ?? 
                throw new ArgumentNullException(nameof(modelMetadataProvider));
        }

        public void Validate(
            ActionContext actionContext, 
            ValidationStateDictionary validationState, 
            string prefix, 
            object model)
        {
            var visitor = new ValidationVisitor(
                actionContext,
                validatorProvider,
                validatorCache,
                modelMetadataProvider,
                validationState);

            var metadata = model == null ? null : modelMetadataProvider.GetMetadataForType(model.GetType());

            visitor.Validate(metadata, prefix, model);
        }
    }
}
