using System;
using Xamarin.Forms;

namespace Custom.Controls
{
    public class Entry : Xamarin.Forms.Entry
    {
        public bool IsFocusedBindable
        {
            get { return (bool)GetValue(IsFocusedBindableProperty); }
            set { SetValue(IsFocusedBindableProperty, value); }
        }

        public bool SuppressSpellCheck
        {
            get { return (bool)GetValue(SuppressSpellCheckProperty); }
            set { SetValue(SuppressSpellCheckProperty, value); }
        }

        public bool SuppressSuggestions
        {
            get { return (bool)GetValue(SuppressSuggestionsProperty); }
            set { SetValue(SuppressSuggestionsProperty, value); }
        }



        public static BindableProperty IsFocusedBindableProperty = BindableProperty.Create(nameof(IsFocusedBindable), typeof(bool),
                            typeof(Entry), default(bool), defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnFocusedBindableChanged);
        public static BindableProperty SuppressSpellCheckProperty = BindableProperty.Create(nameof(SuppressSpellCheck), typeof(bool),
                            typeof(Entry), false, defaultBindingMode: BindingMode.TwoWay, propertyChanged: null);
        public static BindableProperty SuppressSuggestionsProperty = BindableProperty.Create(nameof(SuppressSuggestions), typeof(bool),
                            typeof(Entry), false, defaultBindingMode: BindingMode.TwoWay, propertyChanged: null);

        private static void OnFocusedBindableChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var entry = bindable as Entry;
            if ((bool)newvalue == true)
            {
                entry.Focus();
            }
            else if ((bool)newvalue == false)
            {
                entry.Unfocus();
            }
        }

        private void OnFocusedChanged(object sender, EventArgs args)
        {
            var entry = sender as Entry;
            IsFocusedBindable = entry.IsFocused;
        }


        public Entry()
        {
            base.Unfocused += OnFocusedChanged;
            base.Focused += OnFocusedChanged;
        }
    }
}
