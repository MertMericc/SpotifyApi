using Castle.DynamicProxy;
using FluentValidation;
using SpotifyApi.Core.CrossCuttingConcerns.Validation;
using SpotifyApi.Core.Utilities.Constants;
using SpotifyApi.Core.Utilities.Interceptors;

namespace SpotifyApi.Core.Aspects.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new Exception(AspectMessages.InvalidEmail);
            }
            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType).FirstOrDefault();
            if (entities != null)
            {
                ValidationTool.Validate(validator, entities);
            }
        }
    }
}
