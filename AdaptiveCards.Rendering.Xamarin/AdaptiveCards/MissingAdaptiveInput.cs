using System;
using System.Collections.Generic;
using System.Text;

namespace AdaptiveCards.Rendering.Xamarin
{
    public class MissingAdaptiveInput : AdaptiveInput
    {
        public override string Type { get; set; }

        public string Message { get; set; }

        public override string GetNonInteractiveValue()
        {
            throw new NotImplementedException();
        }
    }
}
