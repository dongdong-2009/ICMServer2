using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

/// This is the base class for all business objects and it provide a change 
/// notfication and basic data validation.
public abstract class BusinessObjectBase : INotifyPropertyChanged, IDataErrorInfo
{
    #region INotifyPropertyChanged members
    // Event fired when the property is changed!
    public event PropertyChangedEventHandler PropertyChanged;
    #endregion

    // This method is called by the Set accessor of each property.
    // The CallerMemberName attribute that is applied to the optional propertyName
    // parameter causes the property name of the caller to be substituted as an argument.
    protected void OnPropertyChanged(String propertyName)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Called when int property in the inherited class is changed for ther others properties like (double, long, or other entities etc,) You have to do it.
    protected void Set<T>(ref T item, T newValue, [CallerMemberName] String propertyName = null)
    {
        if (!EqualityComparer<T>.Default.Equals(item, newValue))
        {
            item = newValue;
            this.Validate(propertyName);
            this.OnPropertyChanged(propertyName);
        }
    }

    /// Validate the property 
    /// <returns>
    /// The list of validation errors
    /// </returns>
    private ICollection<ValidationResult> PropertyValidator(string propertyName)
    {
        var validationResults = new Collection<ValidationResult>();
        PropertyDescriptor property = TypeDescriptor.GetProperties(this)[propertyName];

        Validator.TryValidateProperty(
            property.GetValue(this),
            new ValidationContext(this, null, null) { MemberName = propertyName },
            validationResults);

        return validationResults;
    }

    /// Validates the given property and return all found validation errors.
    protected void Validate(String propertyName)
    {
        var validationResults = this.PropertyValidator(propertyName);
        if (validationResults.Count > 0)
        {
            foreach (var r in validationResults)
            {
                AddError(propertyName, r.ErrorMessage);
            }
        }
        else
            RemoveError(propertyName);
    }

    #region IDataErrorInfo members
    public string Error
    {
        get
        {
            if (validationErrors.Count > 0)
            {
                var errorMessages = validationErrors.Select(v => v.Value);
                return string.Concat(errorMessages);
                //return string.Format("{0} data is invalid.", this.GetType().Name.Split(new char[]{'_'}));
            }
            else
            {
                return null;
            }
        }
    }
      

    public string this[string columnName]
    {
        get
        {
            if (validationErrors.ContainsKey(columnName))
            {
                return validationErrors[columnName].ToString();
            }
            else
            {
                return null;
            }
        }
    }
    #endregion

    // This dictionary contains a list of our validation errors for each field
    private Dictionary<string, string> validationErrors = new Dictionary<string, string>();
    protected void AddError(string columnName, string msg)
    {
        if (!validationErrors.ContainsKey(columnName))
        {
            validationErrors.Add(columnName, msg);
        }
    }

    protected void RemoveError(string columnName)
    {
        if (validationErrors.ContainsKey(columnName))
        {
            validationErrors.Remove(columnName);
        }
    }

    public virtual bool HasErrors
    {
        get { return (validationErrors.Count > 0); }
    }
}