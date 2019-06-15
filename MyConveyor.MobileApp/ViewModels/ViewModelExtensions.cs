using System.Linq;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace MyConveyor.MobileApp.ViewModels
{
    public static class ViewModelExtensions
    {
        public static bool ValidRegexIfNotNull(this string s, string regex)
        {
            return !string.IsNullOrWhiteSpace(s) && Regex.IsMatch(s, regex);
        }

        public static Color RedIfNullOrWhitespace(this string s, BaseViewModel vm)
        {
            return (vm.HasTriedSave && string.IsNullOrWhiteSpace(s)).RedIfInvalid();
        }

        public static Color RedIfInvalid(this bool invalid)
        {
            return invalid ? Color.Red : Color.Transparent;
        }

        public static bool IsAnyEmpty(BaseViewModel vm, params string[] vals)
        {
            return vm.HasTriedSave && vals.Any(x => string.IsNullOrEmpty(x));
        }
    }
}
