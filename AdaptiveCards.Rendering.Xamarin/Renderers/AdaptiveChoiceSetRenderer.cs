using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace AdaptiveCards.Rendering.Xamarin
{
    public static class AdaptiveChoiceSetRenderer
    {
        public static bool CheckBoxUpdating = false;
        public static View Render(AdaptiveChoiceSetInput input, AdaptiveRenderContext context)
        {
            object list;
            string str = input.Value;
            if (str != null)
            {
                list = (
                    from p in str.Split(new char[] { ',' })
                    select p.Trim() into s
                    where !string.IsNullOrEmpty(s)
                    select s).ToList<string>();
            }
            else
            {
                list = null;
            }
            if (list == null)
            {
                list = new List<string>();
            }
            List<string> strs = (List<string>)list;
            StackLayout stackLayout = new StackLayout();
            stackLayout.Orientation = StackOrientation.Vertical;
            foreach (AdaptiveChoice choice in input.Choices)
            {
                if (input.IsMultiSelect)
                {
                    var checkBox = new CheckBox()
                    {
                        Text = choice.Title,
                        IsChecked = strs.Contains(choice.Value),
                        BindingContext = choice,
                        Style = context.GetStyle("Adaptive.Input.AdaptiveChoiceSetInput.CheckBox")
                    };
                    stackLayout.Children.Add(checkBox);


                    checkBox.CheckUnCheckAction = new Action<CheckBox>((c) =>
                    {
                        if (CheckBoxUpdating)
                            return;
                        CheckBoxUpdating = true;

                        if (c.Text == "All")
                            stackLayout.Children.ForEach((i) => (i as CheckBox).IsChecked = c.IsChecked);
                        else if(c.IsChecked)
                        {
                            CheckBox allCheckBox = null;
                            var isChecked = true;
                            foreach(var i in stackLayout.Children)
                            {
                                var box = i as CheckBox;
                                if (box.Text == "All")
                                    allCheckBox = box;
                                else if(!box.IsChecked)
                                {
                                    isChecked = false;
                                    break;
                                }
                            }
                            if (allCheckBox != null && isChecked)
                                allCheckBox.IsChecked = true;
                        }
                        else
                        {
                            foreach (var i in stackLayout.Children)
                            {
                                var box = i as CheckBox;
                                if (box.Text == "All")
                                {
                                    box.IsChecked = false;
                                    break;
                                }
                            }
                        }
                        CheckBoxUpdating = false;
                    });
                }
            }

            context.HideIcon = true;
            context.InputBindings.Add(input.Id, new Func<string>(() =>
            {
                if (input.IsMultiSelect)
                {
                    string value = string.Empty;
                    foreach (var item in stackLayout.Children)
                    {
                        AdaptiveChoice dataContext = item.BindingContext as AdaptiveChoice;
                        var isChecked = (item as CheckBox).IsChecked;
                        if (!isChecked)//(isChecked.GetValueOrDefault() ? !isChecked.HasValue : true))
                        {
                            continue;
                        }
                        value = string.Concat(value, (value == string.Empty ? "" : ";"), dataContext.Value);
                    }
                    return value;
                }
                return string.Empty;
            }));

            return stackLayout;
        }
    }
}
