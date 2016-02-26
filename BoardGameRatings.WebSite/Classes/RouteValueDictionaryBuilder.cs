using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Routing;

namespace BoardGameRatings.WebSite.Classes
{
    public class RouteValueDictionaryBuilder
    {
        public const string ACTION = "action";
        public const string AREA = "area";
        public const string CONTROLLER = "controller";
        private const string CONTROLLER_SUFFIX = "Controller";
        private readonly RouteValueDictionary _dictionary;

        public RouteValueDictionaryBuilder()
        {
            _dictionary = new RouteValueDictionary();
        }

        public RouteValueDictionaryBuilder WithArea(string areaName)
        {
            _dictionary[AREA] = areaName;
            return this;
        }

        public RouteValueDictionaryBuilder WithController(string controllerName)
        {
            if (controllerName.EndsWith(CONTROLLER_SUFFIX))
                controllerName = controllerName.Substring(0, controllerName.Length - CONTROLLER_SUFFIX.Length);
            _dictionary[CONTROLLER] = controllerName;
            return this;
        }

        public RouteValueDictionaryBuilder WithAction(string newActionName)
        {
            _dictionary[ACTION] = newActionName;
            return this;
        }

        public RouteValueDictionaryBuilder WithParameter<T>(string queryParameter, T value)
        {
            _dictionary[queryParameter] = value;
            return this;
        }

        public RouteValueDictionaryBuilder WithListParameter(string queryParameter, IEnumerable<string> values)
        {
            var valuesList = values.ToList();
            for (var i = 0; i < valuesList.Count; i++)
                _dictionary[string.Format("{0}[{1}]", queryParameter, i)] = valuesList[i];
            return this;
        }

        public RouteValueDictionary Build()
        {
            return _dictionary;
        }
    }
}