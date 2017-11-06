using ICMServer.WPF.Validators;
using ReactiveUI;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ICMServer.WPF.ViewModels
{
    public class ValidatableReactiveObject : ReactiveObject, INotifyDataErrorInfo
    {
        protected ReactiveCommand<string, bool> ExecuteValidateProperty { get; set; }

        protected ValidatableReactiveObject()
        {
            ExecuteValidateProperty = ReactiveCommand.CreateFromTask<string, bool>(
                propertyName => ValidatePropertyAsync(propertyName));
            // 覺得驗證器應該要訂閱 PropertyChanged event，處理 propertyName 和 value
            // 然後提供 IObservable供其它屬性訂閱。
        }

        protected ValidatableReactiveObject(IModelValidator validator) : this()
        {
            this._validator = validator;
        }

        /// <summary>
        /// Validate a single property synchronously, by name
        /// </summary>
        /// <typeparam name="TProperty">Type of property to validate</typeparam>
        /// <param name="property">Expression describing the property to validate</param>
        /// <returns>True if the property validated successfully</returns>
        protected virtual bool ValidateProperty<TProperty>(Expression<Func<TProperty>> property)
        {
            return this.ValidateProperty(property.NameForProperty());
        }

        /// <summary>
        /// Validate a single property asynchronously, by name
        /// </summary>
        /// <typeparam name="TProperty">Type ofproperty to validate</typeparam>
        /// <param name="property">Expression describing the property to validate</param>
        /// <returns>True if the property validated successfully</returns>
        protected virtual Task<bool> ValidatePropertyAsync<TProperty>(Expression<Func<TProperty>> property)
        {
            return this.ValidatePropertyAsync(property.NameForProperty());
        }

        /// <summary>
        /// Validate a single property synchronously, by name.
        /// </summary>
        /// <param name="propertyName">Property to validate</param>
        /// <returns>True if the property validated successfully</returns>
        protected bool ValidateProperty([CallerMemberName] string propertyName = null)
        {
            try
            {
                return this.ValidatePropertyAsync(propertyName).Result;
            }
            catch (AggregateException e)
            {
                // We're only ever going to get one InnerException here. Let's be nice and unwrap it
                throw e.InnerException;
            }
        }

        private IModelValidator _validator;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        protected virtual IModelValidator Validator
        {
            get { return this._validator; }
            set
            {
                this._validator = value;
                if (this._validator != null)
                    this._validator.Initialize(this);
            }
        }

        public bool HasErrors
        {
            get { return _errors.Any(x => x.Value.Any()); }
        }

        /// <summary>
        /// Validate a single property asynchronously, by name.
        /// </summary>
        /// <param name="propertyName">Property to validate. Validates the entire model if null or <see cref="String.Empty"/></param>
        /// <returns>True if the property validated successfully</returns>
        /// <remarks>If you override this, you MUST fire ErrorsChanged and call OnValidationStateChanged() if appropriate</remarks>
        protected virtual async Task<bool> ValidatePropertyAsync([CallerMemberName] string propertyName = null)
        {
            if (this.Validator == null)
                throw new InvalidOperationException("Can't run validation if a validator hasn't been set");

            if (propertyName == null)
                propertyName = String.Empty;

            // To allow synchronous calling of this method, we need to resume on the ThreadPool.
            // Therefore, we might resume on any thread, hence the need for a lock
            //if (propertyName == "IPAddress")
            //    DebugLog.TraceMessage(string.Format("{0} :{1}",
            //        propertyName, this.GetType().GetProperty(propertyName).GetValue(this, null)));
            //var newErrorsRaw = await this.Validator.ValidatePropertyAsync(propertyName).ConfigureAwait(false);
            var newErrorsRaw = await this.Validator.ValidatePropertyAsync(propertyName);
            //DebugLog.TraceMessage(string.Format("{1} error count{0}", newErrorsRaw.Count(), propertyName));
            var newErrors = newErrorsRaw == null ? null : newErrorsRaw.ToArray();
            //bool propertyErrorsChanged = false;

            //await this.propertyErrorsLock.WaitAsync();
            //try
            //{
            //    if (!this.propertyErrors.ContainsKey(propertyName))
            //        this.propertyErrors.Add(propertyName, null);

            //    if (!this.ErrorsEqual(this.propertyErrors[propertyName], newErrors))
            //    {
            //        this.propertyErrors[propertyName] = newErrors;
            //        propertyErrorsChanged = true;
            //    }
            //}
            //finally
            //{
            //    this.propertyErrorsLock.Release();
            //}

            //if (propertyErrorsChanged)
            //    this.OnValidationStateChanged(new[] { propertyName });

            return newErrors == null || newErrors.Length == 0;
        }

        public IEnumerable GetErrors(string propertyName)
        {
            List<string> result;
            if (_errors.TryGetValue(propertyName, out result))
            {
                return result;
            }
            return new string[0];
        }

        /// <summary>
        /// Raise the ErrorsChanged event for a given property
        /// </summary>
        /// <param name="propertyName">Property to raise the ErrorsChanged event for</param>
        protected virtual void RaiseErrorsChanged(string propertyName)
        {
            //var handler = this.ErrorsChanged;
            //if (handler != null)
            //    //this.PropertyChangedDispatcher(() => handler(this, new DataErrorsChangedEventArgs(propertyName)));
            //    handler(this, new DataErrorsChangedEventArgs(propertyName));

            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }


        protected readonly ConcurrentDictionary<string, List<string>> _errors =
            new ConcurrentDictionary<string, List<string>>();
    }
}

namespace ReactiveUI
{
    public static class IReactiveObjectExtensions
    {
        public static TRet Set<TObj, TRet>(
               this TObj This,
               ref TRet backingField,
               TRet newValue,
               [CallerMemberName] string propertyName = null) where TObj : IReactiveObject
        {
            return This.RaiseAndSetIfChanged<TObj, TRet>(ref backingField, newValue, propertyName);
        }
    }
}