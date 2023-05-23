using SimpleChatBot.Business.Services.Interfaces;
using System.Reflection;

namespace SimpleChatBot.Business.Services
{
    public class HelperService : IHelperService
    {
        public void TrimStringProperties(object model)
        {
            if (model == null) return;

            PropertyInfo[] properties = model.GetType().GetProperties();
            foreach (var prop in properties)
            {
                if (prop.PropertyType == typeof(string))
                {
                    string value = (string)prop.GetValue(model, null);
                    if (value != null)
                    {
                        prop.SetValue(model, value.Trim(), null);
                    }
                }
            }
        }
    }
}
