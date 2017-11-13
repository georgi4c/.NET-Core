namespace Judge.App.Infrastructure.Validation.Contests
{
    using SimpleMvc.Framework.Attributes.Validation;

    public class ContestNameAttribute : PropertyValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var title = value as string;
            if (title == null)
            {
                return true;
            }

            return title.Length >= 3
                && title.Length <= 100;
        }
    }
}
