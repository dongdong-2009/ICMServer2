using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICMServer.WPF.Validators
{
    public class FluentValidationAdapter<T> : IModelValidator<T>
    {
        private readonly IValidator<T> validator;
        private T subject;

        public FluentValidationAdapter(IValidator<T> validator)
        {
            this.validator = validator;
        }

        public void Initialize(object subject)
        {
            this.subject = (T)subject;
        }

        public async Task<IEnumerable<string>> ValidatePropertyAsync(string propertyName)
        {
            // If someone's calling us synchronously, and ValidationAsync does not complete synchronously,
            // we'll deadlock unless we continue on another thread.
            //return (await Task.Run(() => { return this.validator.Validate(this.subject, propertyName); }))
            //    .Errors.Select(x => x.ErrorMessage);
            var result = await Task.Run(() => {
                //if (propertyName == "IPAddress")
                //    DebugLog.TraceMessage(string.Format("{0} :{1}",
                //        propertyName, this.subject.GetType().GetProperty(propertyName).GetValue(this.subject, null)));
                return this.validator.Validate(this.subject, propertyName);
            });
            //DebugLog.TraceMessage(string.Format("{0}: error count={1}", propertyName, result.Errors.Count));
            //    .Errors.Select(x => x.ErrorMessage);
            return result.Errors.Select(x => x.ErrorMessage);

            //return (await this.validator.ValidateAsync(this.subject, propertyName).ConfigureAwait(false))
            //    .Errors.Select(x => x.ErrorMessage);
        }

        public async Task<Dictionary<string, IEnumerable<string>>> ValidateAllPropertiesAsync()
        {
            // If someone's calling us synchronously, and ValidationAsync does not complete synchronously,
            // we'll deadlock unless we continue on another thread.
            return (await Task.Run(() => { return this.validator.Validate(this.subject); }).ConfigureAwait(false))
                .Errors.GroupBy(x => x.PropertyName)
                .ToDictionary(x => x.Key, x => x.Select(failure => failure.ErrorMessage));

            //return (await this.validator.ValidateAsync(this.subject).ConfigureAwait(false))
            //    .Errors.GroupBy(x => x.PropertyName)
            //    .ToDictionary(x => x.Key, x => x.Select(failure => failure.ErrorMessage));
        }
    }
}
